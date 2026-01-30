using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelect : MonoBehaviour,
    IPointerDownHandler
{
    private CardInteractionState state;

    private Card card;

    [SerializeField] private DeckManager deckManager;

    void Awake()
    {
        state = GetComponent<CardInteractionState>();
        card = GetComponent<Card>();

        deckManager = FindFirstObjectByType<DeckManager>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (deckManager != null)
        {
            deckManager.AddCardToDeck(card.cardData);
        }
        Debug.Log(card.cardData.cardName + " Was Clicked.");
    }
}
