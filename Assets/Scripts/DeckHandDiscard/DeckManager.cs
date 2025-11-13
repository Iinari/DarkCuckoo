using System.Collections;
using System.Collections.Generic;
using SnIProductions;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private List<CardData> allCards = new();

    private List<CardData> startingDeck = new();

    public List<CardData> cardsInPlay = new();

    public int copiesOfStartingCards;

    public int maxHandSize = 10;

    public int startingHandSize = 5;

    private HandManager handManager;

    private DrawPileManager drawPileManager;

    private DiscardManager discardManager;

    private void Start()
    {

        Debug.Log("CardsInPlay length: " + cardsInPlay.Count);
        //Load all card assets from Resources folder
        CardData[] cards = Resources.LoadAll<CardData>("Cards");

        //Add the loaded cards to the allCards list
        allCards.AddRange(cards);

        if (copiesOfStartingCards <= 0) 
        {
            copiesOfStartingCards = 3;
        }


        foreach (CardData card in cards)
        {
            if (card.isInStartingDeck)
            {
                //Checks how many copies for the starting cards should be made and creates the copies
                for (int j = 0; j < copiesOfStartingCards; j++) 
                {
                cardsInPlay.Add(card);
                }
            }
        }

    }

    private void Awake()
    {
        if (drawPileManager == null)
        {
            drawPileManager = FindFirstObjectByType<DrawPileManager>();
        }
        if (handManager == null)
        {
            handManager = FindFirstObjectByType<HandManager>();
        }
        if(discardManager == null)
        {
            discardManager = FindFirstObjectByType<DiscardManager>();
        }
    }


    public void BattleSetup()
    {
        if (drawPileManager == null)
        {
            drawPileManager = FindFirstObjectByType<DrawPileManager>();
        }
        if (handManager == null)
        {
            handManager = FindFirstObjectByType<HandManager>();
        }

        handManager.BattleSetup(maxHandSize);
        drawPileManager.MakeDrawPile(cardsInPlay);
        drawPileManager.BattleSetUp(maxHandSize);
    }

    public void TurnSetUp()
    {
        drawPileManager.StartPlayerTurn(startingHandSize);
    }

    public void EndPlayersTurn()
    {
        if(discardManager == null)
        {
            discardManager = FindFirstObjectByType<DiscardManager>();
        }
        for (int i = 0; i < handManager.cardsInHand.Count; i++)
        {
            discardManager.AddToDiscard(handManager.cardsInHand[i].GetComponent<Card>().cardData);
            Destroy(handManager.cardsInHand[i].gameObject);
        }
        handManager.cardsInHand.Clear();
        handManager.UpdateHandVisuals();
    }
}
