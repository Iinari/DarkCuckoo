using UnityEngine;
using SnIProductions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

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


    public void UpdateCardDisplay()
    {
        nameText.text = cardData.cardName;
        costText.text = cardData.cost.ToString();
        manaCost = cardData.cost;
        descriptionText.text = cardData.description;
        cardImage.sprite = cardData.image;
        cardTypeText.text = cardData.type.ToString();

        UpdateCardDescription();

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
     
}
