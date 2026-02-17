using UnityEngine;
using SnIProductions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.SmartFormat.Utilities;
using UnityEngine.Localization;

//Script for displaying a single card, for the whole hand see HandManager.cs
public class Card : MonoBehaviour
{
    public CardData cardData;

    public Image cardImage;

    public TMP_Text nameText;

    public TMP_Text costText;

    public int manaCost;

    public TMP_Text damageText;

    public TMP_Text descriptionText;

    public TMP_Text cardTypeText;

    public CardPlayManager cardPlayManager;

    public Vector2 cardPlay;

    private CardStringLocalizer[] localizers;

    private CardTypeLocalizer cardTypeLocalizer;

    private void Awake()
    {
        localizers = GetComponentsInChildren<CardStringLocalizer>();
        cardTypeLocalizer = GetComponentInChildren<CardTypeLocalizer>();
    }

    public void UpdateCardDisplay()
    {
        costText.text = cardData.cost.ToString();
        manaCost = cardData.cost;
   
        cardImage.sprite = cardData.image;

        LocalizationUpdate();
    }

    public void UpdateCardDescription()
    {
        switch (cardData.type)
        {
            case CardType.Attack:
                descriptionText.text = cardData.GetCardDescription();
                break;
            case CardType.Skill:
                descriptionText.text = cardData.GetCardDescription();
                break;

        }
    }

    public void LocalizationUpdate()
    {
        for (int i = 0; i < localizers.Length; i++)
        {
            localizers[i].ConstructKey(cardData);
        }
        cardTypeLocalizer.ConstructKey(cardData);
    }
 
}
