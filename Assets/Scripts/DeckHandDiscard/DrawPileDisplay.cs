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

        GenerateCardView(GetCardsFromDrawPileManager());
      
    }

    public void CloseDrawPileDisplay() 
    {
        gameObject.SetActive(false);
    }

    public void GenerateCardView(List<CardData> drawPile)
    {
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

}
