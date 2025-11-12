using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SnIProductions;

public class DiscardManager : MonoBehaviour
{
    public List<CardData> discardCards = new();

    public TextMeshProUGUI discardCountVisual;

    public int discardCount;

    private void Awake()
    {
        UpdateDiscardCount();
    }

    private void UpdateDiscardCount()
    {
        discardCountVisual.text = discardCards.Count.ToString();
        discardCount = discardCards.Count;
    }

    public void AddToDiscard(CardData card)
    {
        if (card != null)
        {
            discardCards.Add(card);
            UpdateDiscardCount();
        }
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
