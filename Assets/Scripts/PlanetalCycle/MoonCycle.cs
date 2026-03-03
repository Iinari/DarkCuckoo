using UnityEngine.UI;
using TMPro;
using UnityEngine;
using SnIProductions;
using UnityEditor.ShaderGraph.Internal;
using Unity.Properties;

public class MoonCycle : MonoBehaviour, IDataPersistence
{
    public float moonCycleLength = 10;

    private float moonCycleMin = 0;

    [CreateProperty]
    public float moonCycleCurrent = 10;

    private BattleStateStatus state;

    public Transform moonPosition;

    public GameObject moonPrefab;

    private MoonVisual moonRef;

    private bool hasBeenUpdated = false;

    private void Awake()
    {
        state = GetComponent<BattleStateStatus>();
        state.OnStateChanged += OnStateChanged;

        GameObject moon = Instantiate(moonPrefab, moonPosition.position, Quaternion.identity, moonPosition);
        moonRef = moon.GetComponent<MoonVisual>();
    }

    public void LoadData(GameData data)
    {
        moonCycleCurrent = data.moonCycle;
    }

    public void SaveData(ref GameData data)
    {
        data.moonCycle = moonCycleCurrent;
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
        if (hasBeenUpdated)
        {
            moonCycleCurrent++;
        }
        
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

        hasBeenUpdated = true;
    }

    void OnDestroy()
    {
        state.OnStateChanged -= OnStateChanged;
    }
}
