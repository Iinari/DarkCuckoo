using SnIProductions;
using UnityEngine;

public class CardHandLayout : MonoBehaviour
{
    public Vector3 TargetPosition { get; private set; }
    public Quaternion TargetRotation { get; private set; }

    private RectTransform rectTransform;
    private CardInteractionState state;

    [SerializeField] private float followSpeed = 10f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        state = GetComponent<CardInteractionState>();
    }

    void Update()
    {
        // Layout does NOTHING while dragging, targeting or playing
        if (state.CurrentState == CardState.Dragging ||
            state.CurrentState == CardState.Playing ||
            state.CurrentState == CardState.Targeting)
            return;

        rectTransform.anchoredPosition = Vector3.Lerp(
            rectTransform.anchoredPosition,
            TargetPosition,
            Time.deltaTime * followSpeed
        );

        rectTransform.localRotation = Quaternion.Lerp(
            rectTransform.localRotation,
            TargetRotation,
            Time.deltaTime * followSpeed
        );
    }

    public void SetTarget(Vector3 pos, Quaternion rot)
    {
        TargetPosition = pos;
        TargetRotation = rot;
    }
}
