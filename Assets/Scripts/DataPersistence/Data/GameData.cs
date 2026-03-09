using System.Collections.Generic;
using SnIProductions;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public List<CardData> cardsInDeck;

    public List<int> cardsInHand;

    public List<CardData> cardsInDrawPile;

    public List<CardData> cardsInDiscard;

    public float moonCycle;

    public int battleState;
}
