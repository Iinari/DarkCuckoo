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
            PlayerHero = newHero.GetComponent<Hero>();
            PlayerHero.SetHeroData(playerData);         
            PlayerHero.GetComponent<AttributesManager>().SetPlayerHeroData(playerData);
        }
    }

    public override void ResumeBattle(BattleInitiator battleSystem)
    {
        //Instatiate the hero
        GameObject newHero = Instantiate(prefab, playerPosition.position, Quaternion.identity, playerPosition);
        PlayerHero = newHero.GetComponent<Hero>();
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
