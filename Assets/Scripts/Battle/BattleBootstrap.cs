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
        Debug.Log("Loading Saved Game");

        yield return null;

        DataPersistenceManager.Instance.LoadGame();

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