using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using SnIProductions;
using System;


public class CardPopUpDisplay : PopUp
{
    public GameObject cardCanvasPrefab;
    public GameObject HBoxPrefab;
    public GameObject contentBorder;
    public Transform contentBorderTransform;
    public GameObject latestHBox;
    public DrawPileManager drawPileManager;
    public DiscardManager discardManager;

    public TextMeshProUGUI header;
    public Button btnBack;

    public void OpenCardDisplay(bool openDiscard)
    {
        gameObject.SetActive(true);

        //First make sure that the Display is clear
        ClearDisplayedCards();


        if (openDiscard)
        {
            GenerateCardView(GetCardsFromDiscardManager());
            header.text = "Discard";
            SetBackButtonRight(btnBack);
        }
        else
        {
            //Then generate display from the cards present in draw pile
            GenerateCardView(GetCardsFromDrawPileManager());
            SetBackButtonLeft(btnBack);
        }
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

    public List<CardData> GetCardsFromDiscardManager()
    {
        if (discardManager == null)
        {
            discardManager = FindFirstObjectByType<DiscardManager>();
        }
        return discardManager.discardCards;
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

    public void SetBackButtonRight(Button button)
    {
        RectTransform rt = button.GetComponent<RectTransform>();

        rt.anchorMin = new Vector2(1f, 0f);
        rt.anchorMax = new Vector2(1f, 0f);
        rt.pivot = new Vector2(1f, 0f);

        rt.anchoredPosition = new Vector2(-5f, -5f); // padding from corner
    }

    public void SetBackButtonLeft(Button button)
    {
        RectTransform rt = button.GetComponent<RectTransform>();

        rt.anchorMin = new Vector2(0f, 0f);
        rt.anchorMax = new Vector2(0f, 0f);
        rt.pivot = new Vector2(0f, 0f);

        rt.anchoredPosition = new Vector2(-5f, -5f); // padding from corner
    }
}
