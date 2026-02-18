using UnityEngine.UI;
using TMPro;
using UnityEngine;
using SnIProductions;
using UnityEditor.ShaderGraph.Internal;
using Unity.Properties;

public class MoonCycle : MonoBehaviour
{
    public float moonCycleLength = 10;

    private float moonCycleMin = 0;

    [CreateProperty]
    public float moonCycleCurrent = 10;

    private BattleStateStatus state;

    public Transform moonPosition;

    public GameObject moonPrefab;

    private MoonVisual moonRef;

    private void Awake()
    {
        state = GetComponent<BattleStateStatus>();
        state.OnStateChanged += OnStateChanged;

        GameObject moon = Instantiate(moonPrefab, moonPosition.position, Quaternion.identity, moonPosition);
        moonRef = moon.GetComponent<MoonVisual>();
    }

    private void OnStateChanged(BattleState newState)
    {
        if (newState == BattleState.PlayerTurn)
        {
            UpdateMoon();
        }
    }
    public void UpdateMoon()
    {
        moonCycleCurrent++;
        if (moonCycleCurrent > moonCycleLength)
        {
            moonCycleCurrent = moonCycleMin;
        }
        if (moonCycleCurrent != moonCycleMin)
        {
            float moonProsent = moonCycleCurrent / moonCycleLength;
            moonRef.FillMoon(moonProsent, moonCycleCurrent);
        }
        else
        {
            moonRef.FillMoon(moonCycleMin, moonCycleCurrent);
        }
    }

    void OnDestroy()
    {
        state.OnStateChanged -= OnStateChanged;
    }

}
