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

    public void FirstPileUpdate(int drawPileCount, string header)
    {
        pileHeader.text = header;
        for (int i = 0; i < flippedCards.Count; i++)
        {
            flippedCards[i].SetActive(drawPileCount - 1 >= i);
        }

        //Depending on the type of the card pile, activate button(s) needed
        switch (header)
        {
            case "Deck":
                btnActivateDiscard.gameObject.SetActive(false);
                break;
            case "Discard":
                btnActivateDrawPile.gameObject.SetActive(false);
                break;
        }
    }
}
