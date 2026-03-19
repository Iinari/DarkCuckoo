using SnIProductions;
using System.Collections.Generic;
using UnityEngine;

public class CardRewardManager : MonoBehaviour
{
    public GameObject cardCanvasPrefab;
    public GameObject contentBorder;
    public GameObject contentBox;
    public int rewardCount;


    public List<GameObject> cardObjs = new();
    private List<CardData> rewardCards = new();

    public void DisplayRewardCards()
    {
        rewardCards.Clear();

        var selectedCards = GetRandomUniqueCards(rewardCount);

        foreach (var card in selectedCards)
        {
            rewardCards.Add(card);
            DisplayCard(card);
        }
    }

    public List<CardData> GetRandomUniqueCards(int count)
    {
        List<CardData> cards = new(CardDatabase.Instance.cardLookup.Values);

        Utility.Shuffle(cards);

        return cards.GetRange(0, Mathf.Min(count, cards.Count));
    }

    public void DisplayCard(CardData card)
    {
        GameObject displayedCard = Instantiate(cardCanvasPrefab, contentBorder.transform.position, Quaternion.identity, contentBorder.transform);
        cardObjs.Add(displayedCard);
        displayedCard.GetComponent<Card>().cardData = card;
        displayedCard.GetComponent<Card>().UpdateCardDisplay();

        displayedCard.GetComponent<CardSelect>().OnCardSelected += HandleCardSelected;
    }

    public void HandleCardSelected()
    {
        foreach (GameObject obj in cardObjs)
        {
            var card = obj.GetComponent<CardSelect>();
            if (card != null)
            {
                card.OnCardSelected -= HandleCardSelected;
            }

            Destroy(obj, 0.2f);
        }

        cardObjs.Clear();
        rewardCards.Clear();

        contentBox.SetActive(false);
    }
}
