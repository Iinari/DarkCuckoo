using SnIProductions;
using UnityEngine;

public class CardPlayVisual : MonoBehaviour
{
    [SerializeField] private GameObject glowEffect;

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

        if(Input.mousePosition.y > card.cardPlay.y)
        {
            glowEffect.SetActive(true);
        }
        else glowEffect.SetActive(false);
    }

}
