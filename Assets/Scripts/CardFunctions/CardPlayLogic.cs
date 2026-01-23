using SnIProductions;
using UnityEngine;

public class CardPlayLogic : MonoBehaviour
{
    [SerializeField] private Vector2 cardPlay;

    [SerializeField] private GameObject playGlowEffect;

    private CardInteractionState state;

    private Card card;

    void Awake()
    {
        state = GetComponent<CardInteractionState>();
        card = GetComponent<Card>();
    }

    void Update()
    {
        if (state.CurrentState != CardState.Dragging)
            return;

        if (Input.mousePosition.y > cardPlay.y && card.cardData.type == CardType.Attack)
        {
            state.SetState(CardState.Targeting);
        }

        /*if (state.CurrentState == CardState.Playing && !Input.GetMouseButton(0))
        {
            TryPlayCard();
            state.ResetToDefault();
        }*/
    }

    private void TryPlayCard()
    {
        Debug.Log("Card played or cancelled");
    }
}
