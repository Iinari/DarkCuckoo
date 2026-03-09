using UnityEngine.UI;
using TMPro;
using UnityEngine;
using SnIProductions;
using UnityEditor.ShaderGraph.Internal;
using Unity.Properties;

public class MoonCycle : MonoBehaviour, IDataPersistence
{
    public float moonCycleLength = 10;

    private readonly float moonCycleMin = 0;

    [CreateProperty]
    public float moonCycleCurrent;

    private readonly float moonCycleDefault = 0;

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

    private void OnEnable()
    {
        if (DataPersistenceManager.Instance == null)
        {
            Debug.LogError("DataPersistenceManager Instance is NULL!");
        }
        else
        {
            DataPersistenceManager.Instance.RegisterDataPersistenceObject(this);
        }
    }

    private void OnDisable()
    {
        DataPersistenceManager.Instance.UnregisterDataPersistenceObject(this);
    }

    public void LoadData(GameData data)
    {
        moonCycleCurrent = data.moonCycle;
        UpdateVisuals();
    }

    public void SaveData(ref GameData data)
    {
        data.moonCycle = moonCycleCurrent;
    }

    public void ResetToDefault(ref GameData data)
    {
        moonCycleCurrent = moonCycleDefault;

        UpdateVisuals();
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
        UpdateVisuals();

        hasBeenUpdated = true;
    }

    void OnDestroy()
    {
        state.OnStateChanged -= OnStateChanged;
    }
    
    public void UpdateVisuals()
    {
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
}
