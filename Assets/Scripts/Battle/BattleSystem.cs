using SnIProductions;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class BattleSystem : MonoBehaviour
{
    //These are values that tester might want to change in editor, that is why these are on top
    public EnemyData ChosenEnemy;

    //variables for displaying and instatiate player hero
    public GameObject playerPrefab;
    public Transform playerPosition;
    public List<HeroData> allHeroes = new();
    public Hero playerHero;

    //variables for displaying and instatiate enemy
    public GameObject enemyPrefab;
    public Transform enemyPosition;
    public List<EnemyData> allEnemies = new();
    public List<Enemy> enemiesInBattle;
    private int enemyCount;

    //Manager classes
    public HeroManager heroManager;
    public EnemyManager enemyManager;
    public CardPlayManager cardPlayManager;
    public DeckManager deckManager;
    public HandManager handManager;
    public DrawPileManager drawPileManager;
    public DiscardManager discardManager;

    private BattleScenePopUpManager popUpManager;

    private BattleStateStatus state;



    void Start()
    {
        SetupBattle();
    }

    public void PlayerTurn()
    {
        state.SetState(BattleState.PlayerTurn);
        RestoreMana();
        deckManager.StartPlayersTurn();

    }

    public void EnemyTurn() 
    {
        state.SetState(BattleState.EnemyTurn);
        deckManager.EndPlayersTurn();

        if (enemiesInBattle.Count <= 0)
        {
            enemiesInBattle.AddRange(GetEncounterEnemies());
        }

        for (int i = 0; i < enemiesInBattle.Count; i++) 
        {
            enemiesInBattle[i].battleSystem = this;
            enemiesInBattle[i].TakeTurn(this);
        }
    }


    public void ChooseEnemy()
    {
        //Enemy can be chosen in Scene, if enemy is not set set randon enemy from the list
        if (ChosenEnemy == null)
        {
            //This is just a testing phase code, later this needs logic for choosing the enemy for right type/level etc.
            EnemyData[] enemies = Resources.LoadAll<EnemyData>("Enemies");
            allEnemies.AddRange(enemies);

            if (allEnemies.Count > 0)
            {
                Utility.Shuffle(allEnemies);
                CreateUnit(allEnemies[0]);
            }
        }
        else
        {
            CreateUnit(ChosenEnemy);
        }
        
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
                case (EnemyData enemyData):
                    enemyManager.DisplayEnemy(enemyData, enemyPrefab, enemyPosition);
                    break;
                default:
                    Debug.Log("Given data for CreateUnit() method in BattleSystem script wasn't hero or enemy");
                    break;
            }
        }
    }

    //Restoring mana for test purposes, later cards/items that restore mana are implemented
    public void RestoreMana()
    {
        //AttributesManager is a class that handles all players attributes
        playerHero.attributesManager.RestoreAttribute(AttributesManager.Attribute.MP);
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

    public List<Enemy> GetEncounterEnemies()
    {
        return enemyManager.enemyList;
    }

    public bool CheckWasDmgLethal()
    {
        return playerHero.attributesManager.hp == 0;
    }

    public void AllEnemiesDied()
    {

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

        enemyManager = FindFirstObjectByType<EnemyManager>();
        heroManager = FindFirstObjectByType<HeroManager>();
        cardPlayManager = FindFirstObjectByType<CardPlayManager>();
        deckManager = FindFirstObjectByType<DeckManager>();
        handManager = FindFirstObjectByType<HandManager>();
        drawPileManager = FindFirstObjectByType<DrawPileManager>();
        discardManager = FindFirstObjectByType<DiscardManager>();

        popUpManager = GetComponent<BattleScenePopUpManager>();
        state = GetComponent<BattleStateStatus>();

        if (enemyManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/EnemyManager");
            if (prefab == null)
            {
                Debug.Log("EnemyManager Prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                enemyManager = GetComponentInChildren<EnemyManager>();
            }
        }

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
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                enemyManager = GetComponentInChildren<EnemyManager>();
            }
        }

        if (handManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/HandManager");
            if (prefab == null)
            {
                Debug.Log("handManager Prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                handManager = GetComponentInChildren<HandManager>();
            }
        }

        popUpManager.SceneUISetUp();

        deckManager.BattleSetup();

        ChooseEnemy();
        ChoosePlayerHero();

        PlayerTurn();
    }

    
}
