using SnIProductions;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static CardDatabase Instance;

    private Dictionary<int, CardData> cardLookup = new();

    private void Awake()
    {
        Instance = this;

        CardData[] cards = Resources.LoadAll<CardData>("ImportedCards");

        foreach (var card in cards)
        {
            cardLookup[card.ID] = card;
        }
    }

    public CardData GetCard(int id)
    {

        if (!cardLookup.TryGetValue(id, out var card))
        {
            Debug.LogError("Card not found: " + id);
        }
        return cardLookup[id];
    }
}