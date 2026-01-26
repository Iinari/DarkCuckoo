using SnIProductions;
using UnityEngine;

public class CardTargeting : MonoBehaviour
{
    private CardInteractionState state;

    [SerializeField] private GameObject playArrow;

    [SerializeField] private GameObject glowEffect;

    void Awake()
    {
        state = GetComponent<CardInteractionState>();
        state.OnStateChanged += OnStateChanged;
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
}
