using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using SnIProductions;
using System;

public class DrawPileDisplay : PopUp
{
    public GameObject cardCanvasPrefab;
    public GameObject HBoxPrefab;
    public GameObject contentBorder;
    public Transform contentBorderTransform;

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
            //GameObject hBox = Instantiate(HBoxPrefab, contentBorderTransform.position, Quaternion.identity, contentBorderTransform);
            //GameObject cardUI = Instantiate(cardCanvasPrefab, contentBorderTransform.position, Quaternion.identity, contentBorderTransform);
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

}
