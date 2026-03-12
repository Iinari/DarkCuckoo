using UnityEngine;
using SnIProductions;
using System.Collections.Generic;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using System;
using UnityEditor.ShaderGraph.Internal;

//Class that handles Players Stats like health, mana, etc. 
public class AttributesManager : MonoBehaviour, IDataPersistence
{
    private Dictionary<StatType, Stat> stats = new(); //Dictionary for all the stats

    public event Action<StatType, Stat> OnStatChanged; //Event that launches when stat is changed (for UI)

    public HeroData heroDefaultData; //Reference to player hero's default data. Data inside the HeroData should not be manipulated

    //Increase or decrease given Stat
    public void ModifyStat(StatType type, float value)
    {
        if (stats.TryGetValue(type, out Stat stat))
        {
            stat.ModifyCurrent(value);

            OnStatChanged?.Invoke(type, stat);
        }
    }

    //Restore given stat to it's maximum value
    public void RestoreStat(StatType type)
    {
        if (stats.TryGetValue(type, out Stat stat))
        {
            stat.Restore();

            OnStatChanged?.Invoke(type, stat);
        }
    }

    //Get stats current value
    public float GetStat(StatType type)
    {
        if (stats.TryGetValue(type, out Stat stat))
            return stat.current;

        return 0;
    }

    //Get stats' maximum value
    public float GetMaxStat(StatType type)
    {
        if (stats.TryGetValue(type, out Stat stat))
            return stat.max;

        return 0;
    }

    //When starting a new run this sets Stats from HeroData 
    //!!! When new stats are implemented like Strength those should be added to stats[]
    public void SetPlayerHeroData(HeroData DataToLoad)
    {
        if (DataToLoad == null)
        {
            Debug.LogWarning("HeroData to load is null");
        }
        else
        {
            heroDefaultData = DataToLoad;
            stats.Clear();

            stats[StatType.Health] = new Stat(heroDefaultData.health, heroDefaultData.health);
            stats[StatType.Mana] = new Stat(heroDefaultData.mana, heroDefaultData.mana);

            BroadcastAllStats();
        }
    }

    //Manually call OnStatChanged, even though the Stat is not actually changed, used when loading from save
    public void BroadcastAllStats()
    {
        foreach (var pair in stats)
        {
            OnStatChanged?.Invoke(pair.Key, pair.Value);
        }
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
