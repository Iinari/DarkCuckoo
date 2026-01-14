using UnityEngine.EventSystems;
using SnIProductions;
using UnityEngine;
using System;
using Mono.Cecil;
using UnityEngine.InputSystem;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public RectTransform rectTransform;

    private RectTransform canvasRectTransform;

    private Canvas canvas;

    private Vector3 originalScale;

    private int currentState = 0;

    private HandManager handManager;

    private DiscardManager discardManager;

    private CardPlayManager playManager;

    private bool isAttackPlayState = false;
    private bool attackCardFrozen = false;

    [HideInInspector] public Vector3 targetHandPos;
    [HideInInspector] public Quaternion targetHandRot;

    [SerializeField] private float selectScale = 1.1f;

    [SerializeField] private Vector2 cardPlay;

    [SerializeField] private Vector3 playPosition;

    [SerializeField] private GameObject glowEffect;

    [SerializeField] private GameObject glowPlayEffect;

    [SerializeField] private GameObject playArrow;

    [SerializeField] private float lerpFactor = 0.1f;

    [SerializeField] private float cardPlayDivider = 2;

    [SerializeField] private float cardPlayMultiplier = 1f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }

        originalScale = canvasRectTransform.localScale;

        UpdateCardPlayPostion();

        handManager = FindFirstObjectByType<HandManager>();
        discardManager = FindFirstObjectByType<DiscardManager>();
        playManager = FindFirstObjectByType<CardPlayManager>();
    }

    

    void Update()
    {
        // Smoothly follow the hand-target when NOT dragging or playing
        if (currentState != 2 && currentState != 3)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(
                rectTransform.anchoredPosition,
                targetHandPos,
                Time.deltaTime * 10f
            );

            rectTransform.localRotation = Quaternion.Lerp(
                rectTransform.localRotation,
                targetHandRot,
                Time.deltaTime * 10f
            );
        }
        switch (currentState)
        {
            case 1:
                HandleHoverState();
                break;
            case 2:
                HandleDragState();
                if(!Input.GetMouseButton(0)) //Check if mouse button is released
                {
                    TransitionToDefaulState();
                }
                break;
            case 3:
                HandlePlayState();
                break;
        }
    }

    //Reset position, scahel and rotation to default
    private void TransitionToDefaulState()
    {
        attackCardFrozen = false;
        currentState = 0;
        rectTransform.localScale = originalScale;

        glowEffect.SetActive(false);
        playArrow.SetActive(false);
        glowPlayEffect.SetActive(false);

        var cg = GetComponent<CanvasGroup>();
        if (cg) cg.blocksRaycasts = true;

        handManager.UpdateHandVisuals();
    }

    //When mouse is hovered over a card
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentState == 0)
        {
            
            originalScale = rectTransform.localScale;

            handManager.UpdateHovered(gameObject);

            currentState = 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            TransitionToDefaulState();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            currentState = 2;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentState != 2) return;

        bool isOverPlayZone = Input.mousePosition.y > cardPlay.y;

        if (!isOverPlayZone) return;
       
        // ENTER PLAY STATE
        currentState = 3;

        if (GetComponent<Card>().cardData.type == CardType.Attack)
        {
            isAttackPlayState = true;
            attackCardFrozen = true;

            playArrow.SetActive(true);
            glowPlayEffect.SetActive(true);

            // Attack cards stop blocking raycasts
            var cg = GetComponent<CanvasGroup>();
            if (!cg) cg = gameObject.AddComponent<CanvasGroup>();
            cg.blocksRaycasts = false;
        }
        else
        {
            isAttackPlayState = false;
            glowPlayEffect.SetActive(true);
        }
    }

    private void HandleHoverState()
    {
        glowEffect.SetActive(true);
        rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, originalScale * selectScale, Time.deltaTime * 10);

        //rectTransform.localScale = originalScale * selectScale;
    }

    private void HandleDragState()
    {
        //Set the card's rotation to zero
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, lerpFactor);

    }

    private void HandlePlayState()
    {
        bool isStillOverPlayZone = Input.mousePosition.y > cardPlay.y;

        // Non-attack cards **follow mouse**
        if (!isAttackPlayState)
        {
            if (isStillOverPlayZone)
                glowPlayEffect.SetActive(true);
            else
                glowPlayEffect.SetActive(false);

            rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, lerpFactor);
            
        }
        else
        {
            // Attack card: FREEZE
            if (!attackCardFrozen)
                attackCardFrozen = true;
        }

        // RELEASE
        if (!Input.GetMouseButton(0))
        {
            Card card = GetComponent<Card>();

            if (playManager.CheckHasEnoughMana(card.manaCost))
            {
                if (card.cardData.type == CardType.Attack)
                    HandleAttackRelease();
                else if (isStillOverPlayZone)
                {
                    PlayNonAttackCard(card);
                }
            }
            TransitionToDefaulState();
        }
    }


    private void UpdateCardPlayPostion()
    {
        if (cardPlayDivider != 0 && canvasRectTransform != null)
        {
            float segment = cardPlayMultiplier / cardPlayDivider;

            cardPlay.y = canvasRectTransform.rect.height * segment;
        }
    }


    public void DiscardThisCard()
    {
        if (discardManager != null)
        {
            discardManager.AddToDiscard(GetComponent<Card>().cardData);
            handManager.cardsInHand.Remove(gameObject);
            Destroy(gameObject);
        }
    }


    private void PlayNonAttackCard(Card card)
    {
        playManager.PlayTheCard(card);
        DiscardThisCard();
    }
    private void HandleAttackRelease()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;

        Collider2D hit = Physics2D.OverlapPoint(worldPoint);

        if (hit != null)
        {
            Enemy enemy = hit.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                playManager.TargetEnemyWithPlay(enemy, GetComponent<Card>());
                DiscardThisCard();
                return;
            }
        }
    }

}
