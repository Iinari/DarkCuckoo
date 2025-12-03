using UnityEngine;
using SnIProductions;
using System.Collections;
using System.Collections.Generic;
using System;

//Script for card hand management 
public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab; //Assign card prefab in inspector

    public Transform handTransform; //Root of the hand position

    public List<GameObject> cardsInHand = new(); //Hold a list of the card objects in our hand

    public float fanSpread = 5f; //Determines how wide cards are spread visually on hand

    public float cardSpacing = 5f; //Determines spacing between cards

    public float verticalSpacing = 100f; //Determines vertical spacing on hand

    public int handStartSize; //Determines how many cards are drwan at start

    public int handMaxSize; // Determines how many cards can be held


    public void AddCardToHand(CardData cardData)
    {
        if (cardsInHand.Count < handMaxSize)
        {
            //Instatiate the card
            GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
            cardsInHand.Add(newCard);

            //Set the CardData of the instantieted card
            
            newCard.GetComponent<Card>().cardData = cardData;
            newCard.GetComponent<Card>().UpdateCardDisplay();

            UpdateHandVisuals();
        }        
    }

    public void BattleSetup(int setMaxHandSize)
    {
        handMaxSize = setMaxHandSize;
    }


    public void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 0) return;

        for (int i = 0; i < cardCount; i++)
        {
            float rot = fanSpread * (i - (cardCount - 1) / 2f);
            float x = cardSpacing * (i - (cardCount - 1) / 2f);

            float t = (2f * i / (cardCount - 1) - 1f);
            float y = verticalSpacing * (1 - t * t);

            CardMovement cm = cardsInHand[i].GetComponent<CardMovement>();

            cm.targetHandPos = new Vector3(x, y, 0f);
            cm.targetHandRot = Quaternion.Euler(0, 0, rot);
        }
    }

    public void UpdateHovered(GameObject hoveredCard)
    {
        int cardCount = cardsInHand.Count;
        if (cardCount == 0) return;

        float pushAmount = 60f;   // how far other cards move aside
        float hoverRise = 60f;   // how much hovered card rises

        int hoveredIndex = cardsInHand.IndexOf(hoveredCard);
        if (hoveredIndex < 0) hoveredIndex = 0; // safety

        for (int i = 0; i < cardCount; i++)
        {
            GameObject cardObj = cardsInHand[i];
            CardMovement cm = cardObj.GetComponent<CardMovement>(); // <-- use cardObj, not hoveredCard

            // Compute base fan layout
            float rot = fanSpread * (i - (cardCount - 1) / 2f);
            float x = cardSpacing * (i - (cardCount - 1) / 2f);

            float t = (2f * i / (cardCount - 1) - 1f);
            float y = verticalSpacing * (1 - t * t);

            if (i == hoveredIndex)
            {
                // Hovered card: rise and straighten
                y += hoverRise;
                cm.targetHandRot = Quaternion.identity;
            }
            else
            {
                // Non-hover: push aside and keep normal rotation
                if (i < hoveredIndex) x -= pushAmount;
                else if (i > hoveredIndex) x += pushAmount;

                cm.targetHandRot = Quaternion.Euler(0f, 0f, rot);
            }

            cm.targetHandPos = new Vector3(x, y, 0f);
        }
    }
}
