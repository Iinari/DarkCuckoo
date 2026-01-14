using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SnIProductions;

public class DiscardManager : MonoBehaviour
{
    public List<CardData> discardCards = new();

    public Transform discardPilePosition;

    public GameObject discardPilePrefab;

    private CardPile discardPileVisual;

    private string discardPileHeader = "Discard";

    private void Awake()
    {
        GameObject visualDiscard = Instantiate(discardPilePrefab, discardPilePosition.position, Quaternion.identity, discardPilePosition);

        discardPileVisual = visualDiscard.GetComponent<CardPile>();

        discardPileVisual.FirstPileUpdate(discardCards.Count, discardPileHeader);
    }

    private void UpdateDiscardCount()
    {
        if (discardPileVisual != null)
        {
            discardPileVisual.UpdatePileVisuals(discardCards.Count);
        }
        else Debug.Log("discardPileVisual is null in DiscardManager");
    }

    public void AddToDiscard(CardData card)
    {
        if (card != null)
        {
            discardCards.Add(card);
            UpdateDiscardCount();
        }
        else Debug.Log("AddToDiscard failed due to null card value");
    }

    public CardData PullFromDiscard()
    {
        if (discardCards.Count > 0)
        {
            CardData cardToReturn = discardCards[discardCards.Count - 1];
            discardCards.RemoveAt(discardCards.Count - 1);
            UpdateDiscardCount();
            return cardToReturn;
        }
        else
        {
            return null;
        }

    }

    public bool PullSelectedCardFromDiscard(CardData card)
    {
        if (discardCards.Count > 0 && discardCards.Contains(card))
        {
            discardCards.Remove(card);
            UpdateDiscardCount();
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<CardData> PullAllFromDiscard() 
    {
        if (discardCards.Count > 0) 
        {
            List<CardData> cardsToReturn = new(discardCards);
            discardCards.Clear();
            UpdateDiscardCount();
            return cardsToReturn;
        }
        else 
        {
            return null;
          
        }
    }

    public void DiscardHand(List<CardData> cardsToDiscard)
    {
        if (cardsToDiscard != null)
        {
            discardCards.AddRange(cardsToDiscard);
            UpdateDiscardCount();
        }
    }
}
