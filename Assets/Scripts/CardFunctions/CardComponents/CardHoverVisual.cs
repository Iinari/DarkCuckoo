using SnIProductions;
using UnityEngine;

public class CardHoverVisual : MonoBehaviour
{
    [SerializeField] private float hoverScale = 1.15f;
    [SerializeField] private float scaleSpeed = 10f;

    [SerializeField] private GameObject glowEffect;

    private CardInteractionState state;
    private Vector3 defaultScale;

    void Awake()
    {
        state = GetComponent<CardInteractionState>();
        defaultScale = transform.localScale;
        state.OnStateChanged += OnStateChanged;
    }

    void Update()
    {
        Vector3 targetScale =
            state.CurrentState == CardState.Hovered
            ? defaultScale * hoverScale
            : defaultScale;

        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * scaleSpeed
        );
    }
    private void OnStateChanged(CardState newState)
    {
        if (newState == CardState.Hovered)
        {
            glowEffect.SetActive(true);
        }
        else
        {
            glowEffect.SetActive(false);
        }
    }
}