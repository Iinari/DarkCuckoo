using SnIProductions;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class BattleInitiator : MonoBehaviour
{
    //variables for displaying and instatiate player hero
    public Hero playerHero;

    //Manager classes
    public BattleScenePopUpManager popUpManager;

    private BattleStateStatus state;
    private BattleComponent[] battleComponents;

    void Start()
    {
        popUpManager = GetComponent<BattleScenePopUpManager>();
        state = GetComponent<BattleStateStatus>();

    }

    //Only to be called in Start()
    public void SetupNewBattle()
    {
        battleComponents = GetComponentsInChildren<BattleComponent>();

        for (int i = 0; i < battleComponents.Length; i++)
        {
            battleComponents[i].BattleSetUp(this);
        }

        state.SetState(BattleState.PlayerTurn);
    }

    //Maybe useless when save system is correclty implemented 
    public void ResumeBattle()
    {
        battleComponents = GetComponentsInChildren<BattleComponent>();

        for (int i = 0; i < battleComponents.Length; i++)
        {
            battleComponents[i].BattleSetUp(this);
        }
    }
}
