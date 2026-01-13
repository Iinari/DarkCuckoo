using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using SnIProductions;
using System;
using static UnityEngine.Rendering.DebugUI;

public class DrawPileDisplay : PopUp
{
    public GameObject cardCanvasPrefab;
    public GameObject HBoxPrefab;
    public GameObject contentBorder;
    public Transform contentBorderTransform;
    public GameObject latestHBox;
    public DrawPileManager drawPileManager;

    public void OpenDrawPileDisplay()
    {
        gameObject.SetActive(true);

        //First make sure that the Display is clear
        ClearDisplayedCards();

        //Then generate display from the cards present in draw pile
        GenerateCardView(GetCardsFromDrawPileManager());
    }

    public void CloseDrawPileDisplay() 
    {
        gameObject.SetActive(false);
    }

    public void GenerateCardView(List<CardData> drawPile)
    {
        Utility.Shuffle(drawPile);
        for (int i = 0; i < drawPile.Count; i++)
        {
            if (i == 0)
            {
                latestHBox = Instantiate(HBoxPrefab, contentBorderTransform.position, Quaternion.identity, contentBorderTransform);
                
            }
            else if (i % 5 == 0) 
            {
                latestHBox = Instantiate(HBoxPrefab, contentBorderTransform.position, Quaternion.identity, contentBorderTransform);
            }

            CreateSingularCardDisplay(latestHBox, drawPile[i]);
        }
    }

    public List<CardData> GetCardsFromDrawPileManager()
    {
        if (drawPileManager == null)
        {
            drawPileManager = FindFirstObjectByType<DrawPileManager>();
        }
        return drawPileManager.drawPile;
    }

    public void CreateSingularCardDisplay(GameObject layoutGroup, CardData card)
    {
        GameObject displayedCard = Instantiate(cardCanvasPrefab, layoutGroup.transform.position, Quaternion.identity, layoutGroup.gameObject.transform);
        displayedCard.GetComponent<Card>().cardData = card;
        displayedCard.GetComponent<Card>().UpdateCardDisplay();
    }

    private void ClearDisplayedCards()
    {
        for (int i = contentBorderTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(contentBorderTransform.GetChild(i).gameObject);
        }

        latestHBox = null;
    }

}
