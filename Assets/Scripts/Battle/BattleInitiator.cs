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
        SetupBattle();
    }

    //Only to be called in Start()
    public void SetupBattle()
    {
        popUpManager = GetComponent<BattleScenePopUpManager>();
        state = GetComponent<BattleStateStatus>();

        battleComponents = GetComponentsInChildren<BattleComponent>();

        for (int i = 0; i < battleComponents.Length; i++)
        {
            battleComponents[i].BattleSetUp(this);
        }

        state.SetState(BattleState.PlayerTurn);
    }
}
