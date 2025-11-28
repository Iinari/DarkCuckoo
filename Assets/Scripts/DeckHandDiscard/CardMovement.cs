using UnityEngine.EventSystems;
using SnIProductions;
using UnityEngine;
using System;
using Mono.Cecil;
using UnityEngine.InputSystem;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;

    private RectTransform canvasRectTransform;

    private Canvas canvas;

    private Vector3 originalScale;

    private int currentState = 0;

    private Quaternion originalRotation;

    private Vector3 originalPosition;

    private HandManager handManager;

    private DiscardManager discardManager;

    private CardPlayManager playManager;

    private bool isAttackPlayState = false;

    [SerializeField] private float selectScale = 1.1f;

    [SerializeField] private Vector2 cardPlay;

    [SerializeField] private Vector3 playPosition;

    [SerializeField] private GameObject glowEffect;

    [SerializeField] private GameObject glowPlayEffect;

    [SerializeField] private GameObject playArrow;

    [SerializeField] private float lerpFactor = 0.1f;

    [SerializeField] private float cardPlayDivider = 2;

    [SerializeField] private float cardPlayMultiplier = 1f;

    [SerializeField] private bool needUpdateCardPlayPosition = false;

    [SerializeField] private int playPositionYDivider = 2;

    [SerializeField] private float playPositionYMultiplier = 1f;

    [SerializeField] private int playPositionXDivider = 4;

    [SerializeField] private float playPositionXMultiplier = 1f;

    [SerializeField] private bool needUpdatePlayPosition = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }

        originalScale = canvasRectTransform.localScale;
        originalPosition = canvasRectTransform.localPosition;
        originalRotation = canvasRectTransform.localRotation;

        UpdateCardPlayPostion();
        UpdatePlayPostion();

        handManager = FindFirstObjectByType<HandManager>();
        discardManager = FindFirstObjectByType<DiscardManager>();
        playManager = FindFirstObjectByType<CardPlayManager>();
    }

    

    void Update()
    {
        if (needUpdateCardPlayPosition)
        {
            UpdateCardPlayPostion();
        }

        if (needUpdatePlayPosition) 
        {
            UpdatePlayPostion();
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
        currentState = 0;
        rectTransform.localScale = originalScale;
        rectTransform.localPosition = originalPosition;
        rectTransform.localRotation = originalRotation;
        glowEffect.SetActive(false);
        playArrow.SetActive(false);
        glowPlayEffect.SetActive(false);

        var cg = GetComponent<CanvasGroup>();
        if (cg) cg.blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentState == 0)
        {
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            originalScale = rectTransform.localScale;

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
        if (currentState == 2)
        {
            bool isOverPlayZone = Input.mousePosition.y > cardPlay.y;

            if (isOverPlayZone)
            {
                currentState = 3;

                glowPlayEffect.SetActive(true);

                if (GetComponent<Card>().cardData.type == CardType.Attack)
                {
                    isAttackPlayState = true;
                    playArrow.SetActive(true);
                }
                else
                {
                    isAttackPlayState = false;
                }
            }
            else
            {
                playArrow.SetActive(false);
                glowPlayEffect.SetActive(false);
            }
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
        bool isOverPlayZoneNow = Input.mousePosition.y > cardPlay.y;

        // Attack cards: freeze card + allow targeting
        if (isAttackPlayState)
        {
            // ensure UI doesn't block physics
            var cg = GetComponent<CanvasGroup>();
            if (!cg) cg = gameObject.AddComponent<CanvasGroup>();
            cg.blocksRaycasts = false;

            // Mouse released → ALWAYS try targeting enemy
            if (!Input.GetMouseButton(0))
            {
                if (playManager.CheckHasEnoughMana(GetComponent<Card>().manaCost))
                {
                    HandleAttackRelease();
                }

                TransitionToDefaulState();
            }

            return;

            /*if (!Input.GetMouseButton(0)) // On release
            {
                bool isStillOverPlayZone = Input.mousePosition.y > cardPlay.y;

                if (isStillOverPlayZone)
                {
                    // PLAY CARD
                    if (playManager.CheckHasEnoughMana(GetComponent<Card>().manaCost))
                    {
                        var card = GetComponent<Card>();

                        switch (card.cardData.type)
                        {
                            case CardType.Attack:
                                HandleAttackRelease();
                                break;

                            default:
                                playManager.PlayTheCard(card);
                                DiscardThisCard();
                                break;
                        }
                    }
                }

                // If attack card misses or non-attack card leaves play zone, reset
                TransitionToDefaulState();
            }*/
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

    private void UpdatePlayPostion()
    {
        if (canvasRectTransform != null && playPositionYDivider != 0 && playPositionXDivider != 0)
        {
            float segmentX = playPositionXMultiplier / playPositionXDivider;
            float segmentY = playPositionYMultiplier / playPositionYDivider;

            playPosition.x = canvasRectTransform.rect.width * segmentX;
            playPosition.y = canvasRectTransform.rect.height * segmentY;
        }
    }

    public void DiscardThisCard()
    {
        if (discardManager != null)
        {
            discardManager.AddToDiscard(GetComponent<Card>().cardData);
            Destroy(gameObject);
        }

        handManager.cardsInHand.Remove(gameObject);
        handManager.UpdateHandVisuals();
    }

    private void HandleAttackRelease()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;

        Collider2D hit = Physics2D.OverlapPoint(worldPoint);

        Debug.Log("WorldPoint: " + worldPoint);

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
