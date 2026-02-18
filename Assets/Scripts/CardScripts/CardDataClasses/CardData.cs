using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace SnIProductions 
{
    public class CardData : ScriptableObject
    {
        public int ID;

        public string cardName;

        public CardType type;

        public bool isInStartDeck;

        public string description;

        public int cost;

        public string spriteName;

        public Rarity rarity;

        public Sprite image;


        public virtual int GetDamage() { return 0; }  // Default implementation

        public virtual string GetCardDescription() { return description; } //Default

        public virtual void UpdateCardDescription() { }

        public virtual int GetHealPower() { return 0; }  // Default implementation

        public virtual int GetBlockPower() { return 0; }  // Default implementation
    }
}


