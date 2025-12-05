using SnIProductions;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawPile : MonoBehaviour
{
    public TextMeshProUGUI drawPileCounter;

    public List<GameObject> flippedCards = new();

    
    public void UpdateDrawPileVisuals(int drawPileCount)
    {
        
        for (int i = 0; i < flippedCards.Count; i++)
        {
            flippedCards[i].SetActive(drawPileCount -1 >= i);
        }
        
    }

}
