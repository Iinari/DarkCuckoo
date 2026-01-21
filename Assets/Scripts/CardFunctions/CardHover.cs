using SnIProductions;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHover : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    private CardInteractionState state;
    private RectTransform rectTransform;
    private Vector3 originalScale;

    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private GameObject glow;

    void Awake()
    {
        state = GetComponent<CardInteractionState>();
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;

        state.OnStateChanged += HandleStateChange;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (state.CurrentState == CardState.Default)
            state.SetState(CardState.Hovered);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (state.CurrentState == CardState.Hovered)
            state.ResetToDefault();
    }

    private void HandleStateChange(CardState newState)
    {
        if (newState == CardState.Hovered)
        {
            rectTransform.localRotation = Quaternion.identity;
        }
        glow.SetActive(newState == CardState.Hovered);
        rectTransform.localScale =
            newState == CardState.Hovered ? originalScale * hoverScale : originalScale;
    }

    void OnDestroy()
    {
        state.OnStateChanged -= HandleStateChange;
    }
}
