using SnIProductions;
using UnityEngine;

public class CardTargeting : MonoBehaviour
{
    private CardInteractionState state;

    [SerializeField] private GameObject playArrow;

    [SerializeField] private GameObject glowEffect;

    private Card card;

    private CardPlayManager playManager;

    private HandManager handManager;

    void Awake()
    {
        state = GetComponent<CardInteractionState>();
        card = GetComponent<Card>();

        playManager = FindFirstObjectByType<CardPlayManager>();
        handManager = FindFirstObjectByType<HandManager>();
        state.OnStateChanged += OnStateChanged;
    }

    private void Update()
    {
        if (state.CurrentState != CardState.Targeting)
            return;

        if (Input.mousePosition.y > card.cardPlay.y && !Input.GetMouseButton(0))
        {
            HandleAttackRelease();
        }
        else if (Input.mousePosition.y < card.cardPlay.y && !Input.GetMouseButton(0))
        {
            state.ResetToDefault();
        }
    }

    void OnDestroy()
    {
        state.OnStateChanged -= OnStateChanged;
    }

    private void OnStateChanged(CardState newState)
    {
        if (newState == CardState.Targeting)
        {
            StartTargeting();
        }
        else
        {
            EndTargeting();
        }
    }

    private void StartTargeting()
    {
        playArrow.SetActive(true);
        glowEffect.SetActive(true);
    }

    private void EndTargeting()
    {
        playArrow.SetActive(false);
        glowEffect.SetActive(false);
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
                playManager.TargetEnemyWithPlay(enemy,gameObject);
                return;
            }
        }
    }
}
