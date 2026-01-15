using SnIProductions;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public GameObject cardCanvasPrefab;
    public GameObject contentBorder;
    public int rewardCount;

    public List<CardData> allRewardCards = new();
    private List<CardData> rewardCards = new();

    public void DisplayRewardCards()
    {
        
        if (allRewardCards.Count == 0)
        {
            CardData[] cards = Resources.LoadAll<CardData>("ImportedCards");
            allRewardCards = new List<CardData>(cards);
        }
        for (int i = 0; i < rewardCount; i++) 
        {
            DisplayCard(GetRandomCard(allRewardCards));
        }
    }

    public CardData GetRandomCard(List<CardData> cards)
    {
        if (cards == null || cards.Count == 0)
        {
            Debug.Log("Null list of cards was given to RewardManagers GetRandomCard");
            return null;
        }

        return cards[Random.Range(0, cards.Count)];
    }

    public void DisplayCard(CardData card)
    {
        GameObject displayedCard = Instantiate(cardCanvasPrefab, contentBorder.transform.position, Quaternion.identity, contentBorder.transform);
        displayedCard.GetComponent<Card>().cardData = card;
        displayedCard.GetComponent<Card>().UpdateCardDisplay();
    }

    public bool SameCardAlreadyDisplayed(CardData card)
    {
        bool alreadyDisplayed = false;
        foreach (CardData c in rewardCards)
        {
            if (c.ID == card.ID)
            {
                alreadyDisplayed = true;
            }
        }
        return alreadyDisplayed;
    }
}
