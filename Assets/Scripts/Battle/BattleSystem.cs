using SnIProductions;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class BattleSystem : MonoBehaviour
{
    //variables for displaying and instatiate player hero
    public GameObject playerPrefab;
    public Transform playerPosition;
    public List<HeroData> allHeroes = new();
    public Hero playerHero;

    //Manager classes
    public HeroManager heroManager;
    public CardPlayManager cardPlayManager;
    public DeckManager deckManager;

    private BattleScenePopUpManager popUpManager;

    private BattleStateStatus state;

    private BattleComponent[] battleComponents;

    void Start()
    {
        SetupBattle();
    }

    public void ChoosePlayerHero()
    {

        HeroData[] playerHeroes = Resources.LoadAll<HeroData>("HeroClasses");
        allHeroes.AddRange(playerHeroes);

        if (allHeroes.Count > 0)
        {
            Utility.Shuffle(allHeroes);
            CreateUnit(allHeroes[0]);
        }
    }

    public void CreateUnit(UnitData unitData)
    {
        if (unitData == null)
        {
            Debug.Log("No data passed to CreateUnit method in BattleSystem");
        }
        else
        {
            switch (unitData)
            {
                case (HeroData heroData):
                    heroManager.DisplayHero(heroData, playerPrefab, playerPosition);
                    break;
 
                default:
                    Debug.Log("Given data for CreateUnit() method in BattleSystem script wasn't hero or enemy");
                    break;
            }
        }
    }

    public void TakeDamage(int incomingDmg)
    {
        if (playerHero == null)
        {
            Debug.Log("No reference to playerHero in BattleSystem while trying to decrease player health");
        }

        playerHero.attributesManager.ModifyAttribute(AttributesManager.Attribute.HP, incomingDmg);

        if (CheckWasDmgLethal())
        {
            if (popUpManager != null)
            {
                popUpManager.OpenResultScreen(true);
            }
        }
    }


    public bool CheckWasDmgLethal()
    {
        return playerHero.attributesManager.hp == 0;
    }


    public void EnemyDied()
    {
        if (popUpManager != null)
        {
            popUpManager.OpenResultScreen(false);
        }
    }

    //Only to be called in Start()
    public void SetupBattle()
    {
        heroManager = FindFirstObjectByType<HeroManager>();
        cardPlayManager = FindFirstObjectByType<CardPlayManager>();
        deckManager = FindFirstObjectByType<DeckManager>();

        popUpManager = GetComponent<BattleScenePopUpManager>();
        state = GetComponent<BattleStateStatus>();

        if (heroManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/HeroManager");
            if (prefab == null)
            {
                Debug.Log("HeroManager Prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                heroManager = GetComponentInChildren<HeroManager>();
            }
        }

        if (cardPlayManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/CardPlayManager");
            if (prefab == null)
            {
                Debug.Log("CardPlayManager Prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                cardPlayManager = GetComponentInChildren<CardPlayManager>();
            }
        }

        if (deckManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/EnemyManager");
            if (prefab == null)
            {
                Debug.Log("EnemyManager Prefab not found");
            }
            else
            {
                //!!
            }
        }

        battleComponents = GetComponentsInChildren<BattleComponent>();

        for (int i = 0; i < battleComponents.Length; i++)
        {
            battleComponents[i].BattleSetUp();
        }

        ChoosePlayerHero();

        state.SetState(BattleState.PlayerTurn);
    }

    
}
