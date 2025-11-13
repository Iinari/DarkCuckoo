using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace SnIProductions 
{
    public class CardData : ScriptableObject
    {
        public string cardName;

        public int ID;

        public bool isInStartingDeck;

        public string cardDescription;

        public int cost;

        public CardType cardType;

        public Rarity cardRarity;

        public Sprite cardImage;


        public virtual int GetDamage() { return 0; }  // Default implementation

        public virtual string GetCardDescription() { return cardDescription; } //Default

        public virtual void UpdateCardDescription() { }

        public virtual int GetHealPower() { return 0; }  // Default implementation

        public virtual int GetBlockPower() { return 0; }  // Default implementation
    }
}


