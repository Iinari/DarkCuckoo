using System.Collections;
using System.Collections.Generic;
using SnIProductions;
using UnityEngine;

//Class for handling players deck, all of it. When cards are added or removed completely (not discarded) it should be done by DeckManager
public class DeckManager : BattleComponent
{
    public List<CardData> deck = new(); //Player's cards

    public int copiesOfStartingCards;
    public int maxHandSize = 10;
    public int startingHandSize = 5;

    private HandManager handManager;
    private DrawPileManager drawPileManager;
    private DiscardManager discardManager;

    private void Start()
    {
        if (deck.Count == 0)
        {
            LoadStartingDeck();
        } 
    }

    public override void BattleSetUp(BattleSystem battleSystem)
    {
        GetManagerReferences();

        handManager.BattleSetup(maxHandSize);
        drawPileManager.MakeDrawPile(deck);
        drawPileManager.BattleSetUp(maxHandSize);
    }

    public void LoadStartingDeck()
    {
        deck.Clear();
        CardData[] cards = Resources.LoadAll<CardData>("ImportedCards");

        //Makes sure copiesOfStartingCards has a value
        if (copiesOfStartingCards <= 0)
        {
            copiesOfStartingCards = 3;
        }

        //Goes through all imported cards to check which are in starting hand.
        //NOTETOSELF: Should Importer put starting cards in different folder?
        foreach (CardData card in cards)
        {
            if (card.isInStartDeck)
            {
                //Checks how many copies for the starting cards should be made and creates the copies
                for (int j = 0; j < copiesOfStartingCards; j++)
                {
                    AddCardToDeck(card);
                }
            }
        }
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

    public void StartPlayersTurn()
    {
        drawPileManager.StartPlayerTurn(startingHandSize);
    }

    public void EndPlayersTurn()
    {
        if(handManager == null)
        {
            GetManagerReferences();
        }
        for (int i = 0; i < handManager.cardsInHand.Count; i++)
        {
            discardManager.AddToDiscard(handManager.cardsInHand[i].GetComponent<Card>().cardData);
            Destroy(handManager.cardsInHand[i]);
        }
        handManager.cardsInHand.Clear();
        handManager.UpdateHandVisuals();
    }

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
}
