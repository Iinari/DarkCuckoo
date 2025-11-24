using SnIProductions;
using UnityEditor.Overlays;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

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
                existingCard.type = card.type;
                existingCard.isInStartDeck = card.isInStartDeck;
                existingCard.cost = card.cost;
                existingCard.rarity = card.rarity;
                existingCard.spriteName = card.spriteName;
                existingCard.description = card.description;
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
            switch (card.type)
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
            attackCard.type = card.type;
            attackCard.isInStartDeck = card.isInStartDeck;
            attackCard.cost = card.cost;
            attackCard.rarity = card.rarity;
            attackCard.spriteName = card.spriteName;
            attackCard.description = card.description;
            return attackCard;


           
        }
        public static void FillAttackData(CardData card, string[] row, string assetPath)
        {
            AttackCardData attackCard = ScriptableObject.CreateInstance<AttackCardData>();
            //base data from the cardData
            attackCard.ID = card.ID;
            attackCard.cardName = card.cardName;
            attackCard.type = card.type;
            attackCard.isInStartDeck = card.isInStartDeck;
            attackCard.cost = card.cost;
            attackCard.rarity = card.rarity;
            attackCard.spriteName = card.spriteName;
            attackCard.description = card.description;

            //AttackCardData type data 
            attackCard.damage = GoogleSheetParser.ParseInt(row[8]);

            AssetDatabase.CreateAsset(attackCard, GetAssetPath(assetPath, card.ID));
        }

        public static void FillSkillData(CardData card, string[] row, string assetPath)
        {
            SkillCardData skillCard = ScriptableObject.CreateInstance<SkillCardData>();
            skillCard.ID = card.ID;
            skillCard.cardName = card.cardName;
            skillCard.type = card.type;
            skillCard.isInStartDeck = card.isInStartDeck;
            skillCard.cost = card.cost;
            skillCard.rarity = card.rarity;
            skillCard.spriteName = card.spriteName;
            skillCard.description = card.description;

            //SkillCardData rows must match the GoogleSheet
            skillCard.block = GoogleSheetParser.ParseInt(row[8]);
            skillCard.heal = GoogleSheetParser.ParseInt(row[9]);

            AssetDatabase.CreateAsset(skillCard, GetAssetPath(assetPath, card.ID));
        }

        //Determines where to create the asset and the naming of the asset
        public static string GetAssetPath(string path, int id)
        {
            return $"{path}Card_{id}.asset";
        }

    } 
}
