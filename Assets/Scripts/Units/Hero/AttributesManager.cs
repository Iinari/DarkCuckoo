using UnityEngine;
using SnIProductions;
using System.Collections.Generic;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using System;
using UnityEditor.ShaderGraph.Internal;

public class AttributesManager : MonoBehaviour, IDataPersistence
{
    private Dictionary<StatType, Stat> stats = new();

    public event Action<StatType, Stat> OnStatChanged;

    public HeroData playerHeroData = null;

    public void ModifyStat(StatType type, float value)
    {
        if (stats.TryGetValue(type, out Stat stat))
        {
            stat.ModifyCurrent(value);

            OnStatChanged?.Invoke(type, stat);
        }
    }

    public void RestoreStat(StatType type)
    {
        if (stats.TryGetValue(type, out Stat stat))
        {
            stat.Restore();

            OnStatChanged?.Invoke(type, stat);
        }
    }

    public float GetStat(StatType type)
    {
        if (stats.TryGetValue(type, out Stat stat))
            return stat.current;

        return 0;
    }

    public float GetMaxStat(StatType type)
    {
        if (stats.TryGetValue(type, out Stat stat))
            return stat.max;

        return 0;
    }

    public void SetPlayerHeroData(HeroData DataToLoad)
    {
        if (DataToLoad == null)
        {
            Debug.LogWarning("HeroData to load is null");
        }
        else
        {
            playerHeroData = DataToLoad;
            stats.Clear();

            stats[StatType.Health] = new Stat(DataToLoad.health, DataToLoad.health);
            stats[StatType.Mana] = new Stat(DataToLoad.mana, DataToLoad.mana);

            BroadcastAllStats();
        }
    }

    public void BroadcastAllStats()
    {
        foreach (var pair in stats)
        {
            OnStatChanged?.Invoke(pair.Key, pair.Value);
        }
    }

    public void InitializeDefaultStats()
    {
        stats.Clear();

        stats[StatType.Health] = new Stat(100, 100);
        stats[StatType.Mana] = new Stat(3, 3);
    }

    //SAVING AND LOADING
    private void OnEnable()
    {
        DataPersistenceManager.Instance.RegisterDataPersistenceObject(this);
    }

    private void OnDisable()
    {
        DataPersistenceManager.Instance.UnregisterDataPersistenceObject(this);
    }

    public void LoadData(GameData data)
    {
        stats.Clear();

        foreach (var s in data.playerStats)
        {
            stats[s.type] = new Stat(s.current, s.max);
        }
        BroadcastAllStats();
    }

    public void SaveData(ref GameData data)
    {
        data.playerStats = new List<StatSaveData>();

        foreach (var stat in stats)
        {
            data.playerStats.Add(new StatSaveData
            {
                type = stat.Key,
                current = stat.Value.current,
                max = stat.Value.max
            });
        }
    }

    public void ResetToDefault(ref GameData data)
    {
        //SetPlayerHeroData(FindFirstObjectByType<Hero>().heroData);
    }

}
