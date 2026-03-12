using UnityEngine;
using System.Collections;

public class BattleBootstrap : MonoBehaviour
{
    void Start()
    {
        if (DataPersistenceManager.Instance.HasLoadedGame)
        {
            StartCoroutine(LoadBattle());
        }
        else
        {
            StartCoroutine(NewBattle());
        }
    }

    IEnumerator NewBattle()
    {
        yield return null;

        SetupBattle();

        //InitializeDefaultState();

        //BroadcastInitialState();
    }

    IEnumerator LoadBattle()
    {
        yield return null;

        DataPersistenceManager.Instance.LoadGame();

        ResumeBattle();

        //BroadcastInitialState();
    }

    void SetupBattle()
    {
        DataPersistenceManager.Instance.ResetSavedDataToDefault();

        BattleInitiator battleInitiator = FindFirstObjectByType<BattleInitiator>();
        battleInitiator.SetupNewBattle();
        /*AttributesManager attributes = FindFirstObjectByType<AttributesManager>();
        attributes.InitializeDefaultStats();

        DeckManager deck = FindFirstObjectByType<DeckManager>();
        deck.InitializeStarterDeck();*/
    }

    void ResumeBattle()
    {
        BattleInitiator battleInitiator = FindFirstObjectByType<BattleInitiator>();
        battleInitiator.ResumeBattle();
    }

    void InitializeDefaultState()
    {

        AttributesManager attributes = FindFirstObjectByType<AttributesManager>();
        attributes.InitializeDefaultStats();

        DeckManager deck = FindFirstObjectByType<DeckManager>();
        deck.InitializeStarterDeck();
    }

    void BroadcastInitialState()
    {
        AttributesManager attributes = FindFirstObjectByType<AttributesManager>();
        attributes.BroadcastAllStats();

        HandManager hand = FindFirstObjectByType<HandManager>();
        hand.UpdateHandVisuals();
    }
}