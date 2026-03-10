using SnIProductions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Overlays;
using UnityEngine;

public class DrawPileManager : MonoBehaviour, IDataPersistence
{
    public List<int> drawPile = new();

    private int maxHandSize;

    private int currentHandSize;

    private HandManager handManager;

    private DiscardManager discardManager;

    public Transform drawPilePosition;

    public GameObject drawPilePrefab;

    private CardPile drawPileVisual;

    void Awake()
    {
        GameObject visualDeck = Instantiate(drawPilePrefab, drawPilePosition.position, Quaternion.identity, drawPilePosition);
        drawPileVisual = visualDeck.GetComponent<CardPile>();    

        handManager = GetComponentInChildren<HandManager>();
    }
    private void OnEnable()
    {
        DataPersistenceManager.Instance?.RegisterDataPersistenceObject(this);
    }

    private void OnDisable()
    {
        DataPersistenceManager.Instance?.UnregisterDataPersistenceObject(this);
    }

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


    public void MakeDrawPile(List<int> cardsToAdd)
    {

        Debug.Log("deck length in draw pile manager: " + cardsToAdd.Count);
        drawPile.Clear();
        foreach (int cardID in cardsToAdd)
        {
            drawPile.Add(cardID);
        }
        Utility.Shuffle(drawPile);

        drawPileVisual.UpdatePileVisuals(drawPile.Count);
    }

    public void BattleSetUp(int setMaxHandSize, int drawAmount)
    {
        if (handManager == null)
        {
            handManager = FindFirstObjectByType<HandManager>();
        }
        maxHandSize = setMaxHandSize;
        
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

        Debug.Log("Last ID: " + drawPile[last]);

        CardData cardData = CardDatabase.Instance.GetCard(cardID);

        if (cardData == null)
        {
            Debug.LogError("Card ID not found: " + cardID);
            return;
        }

        handManager.AddCardToHand(cardData);

        drawPile.RemoveAt(last);

        UpdateDrawPileCount();
    }

    private void UpdateDrawPileCount()
    {
        if (drawPileVisual != null)
        {
            drawPileVisual.UpdatePileVisuals(drawPile.Count);
        }
        else Debug.Log("drawPileVisual is null in DrawPileManager");
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
    public void StartPlayerTurn()
    {
        for (int i = 0; i < handManager.handStartSize; i++)
        {
            DrawCard();
        }
    }
}
