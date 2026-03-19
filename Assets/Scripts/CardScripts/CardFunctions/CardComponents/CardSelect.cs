using SnIProductions;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelect : MonoBehaviour,
    IPointerDownHandler
{
    private CardInteractionState state;

    private Card card;

    public System.Action OnCardSelected;

    void Awake()
    {
        state = GetComponent<CardInteractionState>();
        card = GetComponent<Card>();

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        GameSession.Instance.DeckManager.AddCardToDeck(card.cardData);
        OnCardSelected?.Invoke();
    }
}
