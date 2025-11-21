using SnIProductions;
using UnityEditor.Overlays;
using UnityEditor;
using UnityEngine;

namespace SnIProductions 
{
    public static class ImporterUtility
    {
        public static void CreateCardAsset(CardData card, string savePath)
        {
            string assetPath = $"{savePath}Card_{card.ID}.asset";

            CardData existingCard = AssetDatabase.LoadAssetAtPath<CardData>(assetPath);
            
            //Check if the card asset with this ID already exists, if it does update the data fields if it doesn't create it
            if (existingCard != null)
            {
                // Update fields
                existingCard.cardName = card.cardName;
                existingCard.cardType = card.cardType;
                existingCard.isInStartingDeck = card.isInStartingDeck;
                existingCard.cost = card.cost;
                existingCard.cardRarity = card.cardRarity;
                existingCard.spriteName = card.spriteName;
                existingCard.cardDescription = card.cardDescription;
            }
            else
            {
                //CheckCardType(card);
                AssetDatabase.CreateAsset(GetCardType(card), assetPath);
                //AssetDatabase.CreateAsset(card, assetPath);
            }
        }

        public static void CreateAttackCard(CardData card, string savePath)
        {
            
        }

        public static CardData GetCardType(CardData card)
        {
            switch (card.cardType)
            {
                case CardType.Attack:
                    
                    return SetAttackCardValues(card);
                    
                case CardType.Skill:
                    CardData skillCard = ScriptableObject.CreateInstance<SkillCardData>();
                    return skillCard;
                default:
                    return card;
            }
        }

        public static CardData SetAttackCardValues(CardData card)
        {
            CardData attackCard = ScriptableObject.CreateInstance<AttackCardData>();
            attackCard.cardName = card.cardName;
            attackCard.ID = card.ID;
            attackCard.cardType = card.cardType;
            attackCard.isInStartingDeck = card.isInStartingDeck;
            attackCard.cost = card.cost;
            attackCard.cardRarity = card.cardRarity;
            attackCard.spriteName = card.spriteName;
            attackCard.cardDescription = card.cardDescription;
            return attackCard;
        }
    }
}

