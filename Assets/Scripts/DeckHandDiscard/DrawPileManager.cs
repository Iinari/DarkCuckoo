using SnIProductions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Overlays;
using UnityEngine;

//Part of BattleSystem prefab
public class DrawPileManager : MonoBehaviour, IDataPersistence
{
    public List<int> drawPile = new(); //ID's of cards in drawpile

    public Transform drawPilePosition; //Position in UI, chosen in editor

    public GameObject drawPilePrefab; //Prefab (called DrawPile) chosen in editor

    private HandManager handManager; 

    private DiscardManager discardManager;

    private CardPile drawPileVisual; //Reference to a component of the DrawPile prefab

    private void OnEnable()
    {
        DataPersistenceManager.Instance.RegisterDataPersistenceObject(this);
    }

    private void OnDisable()
    {
        DataPersistenceManager.Instance.UnregisterDataPersistenceObject(this);
    }
    void Awake()
    {
        //Create the draw pile game object based on prefab and given position
        GameObject visualDeck = Instantiate(drawPilePrefab, drawPilePosition.position, Quaternion.identity, drawPilePosition);

        //Save references to variables
        drawPileVisual = visualDeck.GetComponent<CardPile>();    
        handManager = GetComponentInChildren<HandManager>();
        discardManager = GetComponent<DiscardManager>();
    }

    //For the start of players turn draw as many cards as needed
    public void StartPlayerTurn()
    {
        for (int i = 0; i < handManager.handStartSize; i++)
        {
            DrawCard();
        }
    }

    //Fills up the drawpile with new cards. Used when card data is not loaded from save
    public void MakeDrawPile(List<int> cardsToAdd)
    {
        drawPile.Clear();
        foreach (int cardID in cardsToAdd)
        {
            drawPile.Add(cardID);
        }
        Utility.Shuffle(drawPile);

        drawPileVisual.UpdatePileVisuals(drawPile.Count);

        for (int i = 0; i < handManager.handStartSize; i++)
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        if (drawPile.Count == 0)
        {
            RefillDeckFromDiscard();
        }

        if (drawPile.Count == 0)
        {
            Debug.Log("Nothing to draw");
            return;
        }

        if (handManager.handCardObjs.Count >= handManager.handMaxSize)
        {
            Debug.Log("Hand is already full");
            return;
        }
        int last = drawPile.Count - 1;
        int cardID = drawPile[last];

        CardData cardData = CardDatabase.Instance.GetCard(cardID);

        if (cardData == null)
        {
            Debug.LogError("Card ID not found: " + cardID);
            return;
        }

        handManager.AddCardToHand(cardData);

        drawPile.RemoveAt(last);

        drawPileVisual.UpdatePileVisuals(drawPile.Count);
    }

    private void RefillDeckFromDiscard()
    {
        if (discardManager == null)
        {
            discardManager = FindFirstObjectByType<DiscardManager>();
        }

        if(discardManager != null && discardManager.discardCards.Count > 0)
        {
            drawPile = discardManager.PullAllFromDiscard();
            Utility.Shuffle(drawPile);   
        }
    }

    //SAVING AND LOADING:

    public void LoadData(GameData data)
    {
        if (data.cardsInDrawPile == null) return;

        drawPile = new List<int>(data.cardsInDrawPile);
        drawPileVisual.UpdatePileVisuals(drawPile.Count);
    }

    public void SaveData(ref GameData data)
    {
        data.cardsInDrawPile = new List<int>(drawPile);
    }

    public void ResetToDefault(ref GameData data)
    {
        drawPileVisual.UpdatePileVisuals(drawPile.Count);
    }
}
