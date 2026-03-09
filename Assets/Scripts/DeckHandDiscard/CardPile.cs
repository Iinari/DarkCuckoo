using SnIProductions;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardPile : MonoBehaviour
{
    public TextMeshProUGUI pileHeader;

    public TextMeshProUGUI pileCounter;

    public List<GameObject> flippedCards = new();

    public Button btnActivateDrawPile;

    public Button btnActivateDiscard;

    public void UpdatePileVisuals(int cardCount)
    {
        pileCounter.text = cardCount.ToString();
        for (int i = 0; i < flippedCards.Count; i++)
        {
            flippedCards[i].SetActive(cardCount -1 >= i);
        }
        
    }
}
