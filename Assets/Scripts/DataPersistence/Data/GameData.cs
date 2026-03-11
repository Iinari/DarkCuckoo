using System.Collections.Generic;
using SnIProductions;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    //Player Stats
    public List<StatSaveData> playerStats;

    //Cards
    public List<int> cardsInDeck;

    public List<int> cardsInHand;

    public List<int> cardsInDrawPile;

    public List<int> cardsInDiscard;

    

    public float moonCycle;

    public int battleState;
}
