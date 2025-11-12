using SnIProductions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawPileManager : MonoBehaviour
{
    public List<CardData> drawPile = new();

    private int currentIndex = 0;

    private int maxHandSize;

    private int currentHandSize;

    private HandManager handManager;

    private DiscardManager discardManager;

    public TextMeshProUGUI drawPileCounter;

    public void MakeDrawPile(List<CardData> cardsToAdd)
    {
        drawPile.AddRange(cardsToAdd);
        Utility.Shuffle(drawPile);
        UpdateDrawPileCount();
    }

    public void BattleSetUp(int setMaxHandSize)
    {
        if (handManager == null)
        {
            handManager = FindFirstObjectByType<HandManager>();
        }
        maxHandSize = setMaxHandSize;
    }

    public void DrawCard(HandManager handManager)
    {
        if (drawPile.Count == 0)
        {
            RefillDeckFromDiscard();
        }
        
        if (handManager != null && drawPile.Count != 0)
        {
            currentHandSize = handManager.cardsInHand.Count;
            if (currentHandSize < maxHandSize)
            {
                CardData nextCard = drawPile[currentIndex];
                handManager.AddCardToHand(nextCard);
                drawPile.RemoveAt(currentIndex);
                UpdateDrawPileCount();
                if (drawPile.Count > 0) currentIndex %= drawPile.Count;

            }
            else if (handManager.cardsInHand.Count == maxHandSize)
            {
                Debug.Log("Hand is already full");
            }
        }
        else Debug.Log("Nothing to draw");

    }

    private void UpdateDrawPileCount()
    {
        drawPileCounter.text = drawPile.Count.ToString();
    }

    private void RefillDeckFromDiscard()
    {
        if (discardManager == null)
        {
            discardManager = FindFirstObjectByType<DiscardManager>();
        }

        if(discardManager != null && discardManager.discardCount > 0)
        {
            drawPile = discardManager.PullAllFromDiscard();
            Utility.Shuffle(drawPile);
            currentIndex = 0;
           
        }
    }
    public void StartPlayerTurn(int drawCount)
    {
        for (int i = 0; i < drawCount; i++)
        {
            DrawCard(handManager);
        }
    }
}
