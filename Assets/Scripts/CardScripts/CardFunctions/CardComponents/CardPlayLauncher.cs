using SnIProductions;
using UnityEngine;

public class CardPlayLauncher : MonoBehaviour
{
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

        if (Input.mousePosition.y > card.cardPlay.y && card.cardData.type == CardType.Attack)
        {
            state.SetState(CardState.Targeting);
        }
        if (Input.mousePosition.y > card.cardPlay.y && !Input.GetMouseButton(0)) 
        {
            TryPlayCard();
        }
        else if (Input.mousePosition.y < card.cardPlay.y && !Input.GetMouseButton(0))
        {
            state.ResetToDefault();
        }
    }

    private void TryPlayCard()
    {
        if (BattleContext.Instance.cardPlayManager.CheckHasEnoughMana(card.cardData.cost))
        {
            BattleContext.Instance.cardPlayManager.PlayTheCard(gameObject);
        } 
        else state.ResetToDefault();
    }
}
