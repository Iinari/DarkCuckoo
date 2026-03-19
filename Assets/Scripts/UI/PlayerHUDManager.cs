using System.Linq;
using UnityEngine;
using SnIProductions;
using System.Collections.Generic;
using static UnityEngine.Rendering.GPUSort;
using UnityEditor.EditorTools;

public class PlayerHUDManager : MonoBehaviour
{
    public Hero PlayerHero { get; private set; }

    private HeroData playerData;

    public Transform playerPosition;

    public GameObject prefab;

    private List<HeroData> allHeroes = new();


    private void OnEnable()
    {
        BattleEvents.OnBattleStarted += NewBattle;
        BattleEvents.OnBattleLoaded += ResumeBattle;
    }

    private void OnDisable()
    {
        BattleEvents.OnBattleStarted -= NewBattle;
        BattleEvents.OnBattleLoaded -= ResumeBattle;

    }

    void NewBattle()
    {
        playerData = LoadPlayerDataFormSciptableObjects()[0];

        //Instatiate the hero
        GameObject newHero = Instantiate(prefab, playerPosition.position, Quaternion.identity, playerPosition);

        if (newHero.GetComponent<Hero>() == null)
        {
            Debug.Log("instantiated object didn't have Hero component");
        }
        else
        {
            PlayerHero = newHero.GetComponent<Hero>();
            PlayerHero.SetHeroData(playerData);
            GameSession.Instance.AttributesManager.SetPlayerHeroData(playerData);
        }
    }
   
    public void ResumeBattle()
    {
        // Data already loaded via IDataPersistence

        GameObject newHero = Instantiate(prefab, playerPosition.position, Quaternion.identity, playerPosition);

        PlayerHero = newHero.GetComponent<Hero>();

        // Reconnect references, visuals, runtime-only things
        BattleContext.Instance.playerHero = PlayerHero;
    }

    public HeroData GetRandonmPlayerStats()
    {
        HeroData[] playerHeroes = Resources.LoadAll<HeroData>("HeroClasses");
        allHeroes.AddRange(playerHeroes);

        if (allHeroes.Count > 0)
        {
            Utility.Shuffle(allHeroes);
            return allHeroes[0];
        }

        return null;
    }

    public HeroData[] LoadPlayerDataFormSciptableObjects()
    {
        return Resources.LoadAll<HeroData>("PlayerData");
    }
}
