using UnityEngine;
using SnIProductions;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.XR;
using static UnityEngine.Rendering.GPUSort;

//Script for card hand management 
public class HandManager : MonoBehaviour, IDataPersistence
{
    public GameObject cardPrefab; //Assign card prefab in inspector

    public Transform handTransform; //Root of the hand position

    public List<GameObject> handCardObjs = new(); //Hold a list of the card objects in our hand

    public float fanSpread = 5f; //Determines how wide cards are spread visually on hand

    public float cardSpacing = 5f; //Determines spacing between cards

    public float verticalSpacing = 100f; //Determines vertical spacing on hand

    public int handStartSize; //Determines how many cards are drwan at start, this value might change during run
    private readonly int defaulthandSize = 5; //Same as above, but this never changes

    public int handMaxSize; // Determines how many cards can be held, this value might change during run
    private readonly int defaultMaxhandSize = 10; //Same as above, but this never changes

    private Dictionary<CardInteractionState, Action<CardState>> stateHandlers
        = new();

    private void OnEnable()
    {
        DataPersistenceManager.Instance.RegisterDataPersistenceObject(this);
    }

    private void OnDisable()
    {
        DataPersistenceManager.Instance.UnregisterDataPersistenceObject(this);
    }

    public void AddCardToHand(CardData cardData)
    {
        if (handCardObjs.Count >= handMaxSize) return;

        CreateHandCard(cardData);
    }

    public void RegisterCard(GameObject card)
    {
        handCardObjs.Add(card);

        if (card.TryGetComponent<CardInteractionState>(out var state))
        {
            void handler(CardState _) => UpdateHandVisuals();
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
        handCardObjs.Remove(card);
        UpdateHandVisuals();

        Destroy(card);
    }

    private void CreateHandCard(CardData cardData)
    {
        GameObject newCard = Instantiate(cardPrefab, handTransform);

        Card card = newCard.GetComponent<Card>();
        card.cardData = cardData;
        card.UpdateCardDisplay();

        RegisterCard(newCard);
    }

    //Clear and unregister hand cards
    private void ClearHand()
    {
        while (handCardObjs.Count > 0)
        {
            UnregisterCard(handCardObjs[0]);
        }
    }

    //Adding or removing/discarding a card from hands affects the spread of other hand cards
    public void UpdateHandVisuals()
    {
        int cardCount = handCardObjs.Count;
        if (cardCount == 0) return;

        if (cardCount == 1)
        {
            var layout = handCardObjs[0].GetComponent<CardHandLayout>();
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

            var layout = handCardObjs[i].GetComponent<CardHandLayout>();
            layout.SetTarget(new Vector3(x, y, 0f), Quaternion.Euler(0, 0, rot));
        }
    }

    //Hovering of one card effects the other cards in hand
    public void UpdateHovered(GameObject hoveredCard)
    {
        int cardCount = handCardObjs.Count;
        if (cardCount == 0) return;

        float pushAmount = 60f;
        float hoverRise = 60f;

        int hoveredIndex = handCardObjs.IndexOf(hoveredCard);
        if (hoveredIndex < 0) return;

        // Single card
        if (cardCount == 1)
        {
            var layout = handCardObjs[0].GetComponent<CardHandLayout>();
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

            var layout = handCardObjs[i].GetComponent<CardHandLayout>();
            layout.SetTarget(new Vector3(x, y, 0f), Quaternion.Euler(0, 0, rot));
        }
    }

    //SAVING AND LOADING
    public void LoadData(GameData data)
    {
        ClearHand();

        if (data.cardsInHand == null) return;

        foreach (var id in data.cardsInHand)
        {
            CardData cardData = CardDatabase.Instance.GetCard(id);
            AddCardToHand(cardData);
        }
    }

    public void SaveData(ref GameData data)
    {
        List<int> cardIDs = new List<int>();
   
        foreach (var obj in handCardObjs)
        {
            var card = obj.GetComponent<Card>();
            cardIDs.Add(card.cardData.ID);
        }
        data.cardsInHand = cardIDs;
    }

    public void ResetToDefault(ref GameData data)
    {
        ClearHand();

        data.cardsInHand = new List<int>();

        handStartSize = defaulthandSize;
        handMaxSize = defaultMaxhandSize;

        UpdateHandVisuals();
    }
}
