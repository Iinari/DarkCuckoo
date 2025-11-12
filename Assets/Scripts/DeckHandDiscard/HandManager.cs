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

        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));

            float normalizedPosition = (2f * i / (cardCount - 1) - 1f); //Normalize card position between -1, 1
            float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition); 

            //Set card position
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }
    }

}
        