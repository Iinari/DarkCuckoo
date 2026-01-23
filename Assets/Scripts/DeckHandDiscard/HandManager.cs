using UnityEngine;
using SnIProductions;
using System.Collections;
using System.Collections.Generic;
using System;

//Script for card hand management 
public class HandManager : MonoBehaviour
{
    public bool usingNewSystem;

    public GameObject cardPrefab; //Assign card prefab in inspector

    public GameObject newCardPrefab;

    public Transform handTransform; //Root of the hand position

    public List<GameObject> cardsInHand = new(); //Hold a list of the card objects in our hand

    public float fanSpread = 5f; //Determines how wide cards are spread visually on hand

    public float cardSpacing = 5f; //Determines spacing between cards

    public float verticalSpacing = 100f; //Determines vertical spacing on hand

    public int handStartSize; //Determines how many cards are drwan at start

    public int handMaxSize; // Determines how many cards can be held

    private Dictionary<CardInteractionState, Action<CardState>> stateHandlers
        = new();

    

    public void AddCardToHand(CardData cardData)
    {
        if (cardsInHand.Count < handMaxSize)
        {

            //Instatiate the card
            if (usingNewSystem)
            {
                GameObject newCard = Instantiate(newCardPrefab, handTransform.position, Quaternion.identity, handTransform);

                //Set the CardData of the instantieted card

                newCard.GetComponent<Card>().cardData = cardData;
                newCard.GetComponent<Card>().UpdateCardDisplay();
                RegisterCard(newCard);
            }
            else
            {
                GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
                cardsInHand.Add(newCard);

                //Set the CardData of the instantieted card

                newCard.GetComponent<Card>().cardData = cardData;
                newCard.GetComponent<Card>().UpdateCardDisplay();
                UpdateHandVisuals();
            } 
        }        
    }

    public void BattleSetup(int setMaxHandSize)
    {
        handMaxSize = setMaxHandSize;
    }


    public void UpdateHandVisuals()
    {
        if (usingNewSystem)
        {
            int cardCount = cardsInHand.Count;
            if (cardCount == 0) return;

            if (cardCount == 1)
            {
                var layout = cardsInHand[0].GetComponent<CardHandLayout>();
                layout.SetTarget(Vector3.zero, Quaternion.identity);
                return;
            }

            float centerOffset = (cardCount - 1) / 2f;

            for (int i = 0; i < cardCount; i++)
            {
                float rot = fanSpread * (i - centerOffset);
                float x = cardSpacing * (i - centerOffset);

                float t = (2f * i / (cardCount - 1) - 1f);
                float y = verticalSpacing * (1 - t * t);

                var layout = cardsInHand[i].GetComponent<CardHandLayout>();
                layout.SetTarget(new Vector3(x, y, 0f), Quaternion.Euler(0, 0, rot));
            }
        }
        else OldUpdateVisuals();
    }

    public void UpdateHovered(GameObject hoveredCard)
    {
        if (usingNewSystem)
        {
            int cardCount = cardsInHand.Count;
            if (cardCount == 0) return;

            float pushAmount = 60f;
            float hoverRise = 60f;

            int hoveredIndex = cardsInHand.IndexOf(hoveredCard);
            if (hoveredIndex < 0) return;

            // Single card
            if (cardCount == 1)
            {
                var layout = cardsInHand[0].GetComponent<CardHandLayout>();
                layout.SetTarget(new Vector3(0f, hoverRise, 0f), Quaternion.identity);
                return;
            }

            float centerOffset = (cardCount - 1) / 2f;

            for (int i = 0; i < cardCount; i++)
            {
                float rot = fanSpread * (i - centerOffset);
                float x = cardSpacing * (i - centerOffset);

                float t = (2f * i / (cardCount - 1) - 1f);
                float y = verticalSpacing * (1 - t * t);

                if (i == hoveredIndex)
                {
                    y += hoverRise;
                    rot = 0f;
                }
                else
                {
                    if (i < hoveredIndex) x -= pushAmount;
                    else if (i > hoveredIndex) x += pushAmount;
                }

                var layout = cardsInHand[i].GetComponent<CardHandLayout>();
                layout.SetTarget(new Vector3(x, y, 0f), Quaternion.Euler(0, 0, rot));
            }
        }
        else OldUpdateHovered(hoveredCard);
    }

    public void OldUpdateVisuals()
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 0) return;

        // Special case: single card
        if (cardCount == 1)
        {
            CardMovement cm = cardsInHand[0].GetComponent<CardMovement>();
            cm.targetHandPos = Vector3.zero;
            cm.targetHandRot = Quaternion.identity;
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            float centerOffset = (cardCount - 1) / 2f;

            float rot = fanSpread * (i - centerOffset);
            float x = cardSpacing * (i - centerOffset);

            float t = (2f * i / (cardCount - 1) - 1f);
            float y = verticalSpacing * (1 - t * t);

            CardMovement cm = cardsInHand[i].GetComponent<CardMovement>();
            cm.targetHandPos = new Vector3(x, y, 0f);
            cm.targetHandRot = Quaternion.Euler(0, 0, rot);
        }
    }

    public void OldUpdateHovered(GameObject hoveredCard)
    {
        int cardCount = cardsInHand.Count;
        if (cardCount == 0) return;

        float pushAmount = 60f;
        float hoverRise = 60f;

        int hoveredIndex = cardsInHand.IndexOf(hoveredCard);
        if (hoveredIndex < 0) hoveredIndex = 0;

        // 🔹 SPECIAL CASE: only one card
        if (cardCount == 1)
        {
            CardMovement cm = cardsInHand[0].GetComponent<CardMovement>();

            cm.targetHandPos = new Vector3(0f, hoverRise, 0f);
            cm.targetHandRot = Quaternion.identity;

            return;
        }

        float centerOffset = (cardCount - 1) / 2f;

        for (int i = 0; i < cardCount; i++)
        {
            GameObject cardObj = cardsInHand[i];
            CardMovement cm = cardObj.GetComponent<CardMovement>();

            float rot = fanSpread * (i - centerOffset);
            float x = cardSpacing * (i - centerOffset);

            float t = (2f * i / (cardCount - 1) - 1f);
            float y = verticalSpacing * (1 - t * t);

            if (i == hoveredIndex)
            {
                y += hoverRise;
                cm.targetHandRot = Quaternion.identity;
            }
            else
            {
                if (i < hoveredIndex) x -= pushAmount;
                else if (i > hoveredIndex) x += pushAmount;

                cm.targetHandRot = Quaternion.Euler(0f, 0f, rot);
            }

            cm.targetHandPos = new Vector3(x, y, 0f);
        }
    }


    public void RegisterCard(GameObject card)
    {
        cardsInHand.Add(card);

        var state = card.GetComponent<CardInteractionState>();
        if (state != null)
        {
            Action<CardState> handler = _ => UpdateHandVisuals();
            stateHandlers[state] = handler;
            state.OnStateChanged += handler;
        }

        UpdateHandVisuals();
    }

    public void UnregisterCard(GameObject card)
    {
        var state = card.GetComponent<CardInteractionState>();
        if (state != null && stateHandlers.TryGetValue(state, out var handler))
        {
            state.OnStateChanged -= handler;
            stateHandlers.Remove(state);
        }

        cardsInHand.Remove(card);
        UpdateHandVisuals();
    }
}
