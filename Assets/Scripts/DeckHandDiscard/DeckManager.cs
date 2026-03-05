using System.Collections;
using System.Collections.Generic;
using SnIProductions;
using UnityEngine;

//Class for handling players deck, all of it. When cards are added or removed completely (not discarded) it should be done by DeckManager
public class DeckManager : MonoBehaviour, IDataPersistence
{
    public List<CardData> deck = new(); //Player's cards

    public int maxHandSize = 10;
    public int startingHandSize = 5;

    private HandManager handManager;
    private DrawPileManager drawPileManager;
    private DiscardManager discardManager;

    private void OnEnable()
    {
        DataPersistenceManager.Instance.RegisterDataPersistenceObject(this);
    }

    private void OnDisable()
    {
        DataPersistenceManager.Instance?.UnregisterDataPersistenceObject(this);
    }

    public void LoadData(GameData data)
    {
        deck = data.cardsInDeck;

        if (deck.Count == 0)
        {
            Debug.Log("No data on existing deck");
            deck = GetComponent<DefaultDeckCreator>().LoadStartingDeck();
        }
        BattleSetUp();
    }

    public void SaveData(ref GameData data)
    {
        data.cardsInDeck = deck;
    }

    public void BattleSetUp()
    {
        GetManagerReferences();

        handManager.BattleSetup(maxHandSize);
        drawPileManager.MakeDrawPile(deck);
        drawPileManager.BattleSetUp(maxHandSize, startingHandSize);
    }

    public void AddCardToDeck(CardData card)
    {
        deck.Add(card);
    }

    /**
    public void RemoveCardFromDeck(CardData card) 
    { 
        deck.Remove(card);
    }**/


    public void GetManagerReferences()
    {
        if (drawPileManager == null)
        {
            drawPileManager = FindFirstObjectByType<DrawPileManager>();
        }
        if (handManager == null)
        {
            handManager = FindFirstObjectByType<HandManager>();
        }
        if (discardManager == null)
        {
            discardManager = FindFirstObjectByType<DiscardManager>();
        }
    }

    public void ResetDeck()
    {
        deck = GetComponent<DefaultDeckCreator>().LoadStartingDeck();
        FindAnyObjectByType<DataPersistenceManager>().SaveGame();
    }
}
