using NUnit.Framework;
using SnIProductions;
using System.Collections.Generic;
using UnityEngine;

public class DefaultDeckCreator : MonoBehaviour
{
    public int copiesOfStartingCards = 3;
    public List<int> LoadStartingDeck()
    {
        CardData[] cards = Resources.LoadAll<CardData>("ImportedCards");

        //Makes sure copiesOfStartingCards has a value
        if (copiesOfStartingCards <= 0)
        {
            copiesOfStartingCards = 3;
        }
        List<int> deck = new();
        //Goes through all imported cards to check which are in starting hand.
        //NOTETOSELF: Should Importer put starting cards in different folder?
        foreach (CardData card in cards)
        {
            if (card.isInStartDeck)
            {
                //Checks how many copies for the starting cards should be made and creates the copies
                for (int j = 0; j < copiesOfStartingCards; j++)
                {
                    deck.Add(card.ID);
                }
            }
        }
        return deck;
    }
}
