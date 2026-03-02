using System.Linq;
using UnityEngine;
using SnIProductions;
using System.Collections.Generic;

public class PlayerHUDManager : BattleComponent
{
    public Hero PlayerHero { get; private set; }

    private HeroData playerData;

    public Transform playerPosition;

    public GameObject prefab;

    private List<HeroData> allHeroes = new();
    public override void BattleSetUp(BattleInitiator battleSystem)
    {
        playerData = LoadPlayerData()[0];

        //Instatiate the hero
        GameObject newHero = Instantiate(prefab, playerPosition.position, Quaternion.identity, playerPosition);

        if (newHero.GetComponent<Hero>() == null)
        {
            Debug.Log("instantiated object didn't have Hero component");
        }
        else
        {
            newHero.GetComponent<Hero>().HeroDisplayFirstUpdate(playerData);
            PlayerHero = newHero.GetComponent<Hero>();
            battleSystem.playerHero = PlayerHero;
        }
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

    public HeroData[] LoadPlayerData()
    {
        return Resources.LoadAll<HeroData>("PlayerData");
    }
}
