using System.Collections;
using System.Collections.Generic;
using SnIProductions;
using Unity.VisualScripting;
using UnityEngine;

//Class for handling players deck, all of it. When cards are added or removed completely (not discarded) it should be done by DeckManager
public class DeckManager : MonoBehaviour, IDataPersistence
{
    public List<int> deck = new(); //Player's cards

    private DrawPileManager drawPileManager;

    private void OnEnable()
    {
        DataPersistenceManager.Instance.RegisterDataPersistenceObject(this);
        BattleEvents.OnBattleStarted += NewBattle;
    }

    private void OnDisable()
    {
        DataPersistenceManager.Instance.UnregisterDataPersistenceObject(this);
        BattleEvents.OnBattleStarted -= NewBattle;
    }

    public void NewBattle()
    {
        InitializeStarterDeck();
    }

    //Add a new card to deck
    public void AddCardToDeck(CardData card)
    {
        deck.Add(card.ID);
    }

    //Removing card from deck
    public void RemoveCardFromDeck(CardData card)
    {
        deck.Remove(card.ID);
    }

    public void InitializeStarterDeck()
    {
        deck.Clear();
        deck = BattleContext.Instance.defaultDeckCreator.LoadStartingDeck();
        BattleContext.Instance.drawPileManager.MakeDrawPile(deck);
    }

    //FOR TEST PURPOSES 
    public void ResetAll()
    {
        FindAnyObjectByType<DataPersistenceManager>().ResetDataToDefault();
    }

    //SAVING AND LOADING

    

    public void LoadData(GameData data)
    {
        deck = data.cardsInDeck;
    }

    public void SaveData(ref GameData data)
    {
        if (data != null)
        {
            data.cardsInDeck = deck;
        }
    }

    public void ResetToDefault(ref GameData data)
    {
        deck = BattleContext.Instance.defaultDeckCreator.LoadStartingDeck();
        data.cardsInDeck = deck;
        BattleContext.Instance.drawPileManager.MakeDrawPile(deck);
 
    }
}
