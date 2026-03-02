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
    private BattleScenePopUpManager popUpManager;

    private BattleStateStatus state;
    private BattleComponent[] battleComponents;

    void Start()
    {
        SetupBattle();
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

        battleComponents = GetComponentsInChildren<BattleComponent>();

        for (int i = 0; i < battleComponents.Length; i++)
        {
            battleComponents[i].BattleSetUp();
        }

        state.SetState(BattleState.PlayerTurn);
    }

    
}
