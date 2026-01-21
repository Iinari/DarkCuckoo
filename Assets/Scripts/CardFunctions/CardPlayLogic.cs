using SnIProductions;
using UnityEngine;

public class CardPlayLogic : MonoBehaviour
{
    [SerializeField] private Vector2 cardPlay;

    private CardInteractionState state;

    void Awake()
    {
        state = GetComponent<CardInteractionState>();
    }

    void Update()
    {
        if (state.CurrentState != CardState.Dragging)
            return;

        if (Input.mousePosition.y > cardPlay.y)
        {
            state.SetState(CardState.Playing);
        }

        if (state.CurrentState == CardState.Playing && !Input.GetMouseButton(0))
        {
            TryPlayCard();
            state.ResetToDefault();
        }
    }

    private void TryPlayCard()
    {
        Debug.Log("Card played or cancelled");
    }
}
