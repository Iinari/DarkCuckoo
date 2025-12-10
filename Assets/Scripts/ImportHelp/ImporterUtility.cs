using SnIProductions;
using UnityEditor.Overlays;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using System;

namespace SnIProductions
{
    public static class ImporterUtility
    {
        public static void CreateAttackCardSO(CardData card, string[] row, string assetPath)
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
            attackCard.image = LoadCardSprite(card.spriteName);
            //AttackCardData type data 
            attackCard.damage = GoogleSheetParser.ParseInt(row[8]);

            AssetDatabase.CreateAsset(attackCard, GetAssetPath(assetPath, card.ID));
        }

        public static void CreateSkillCardSO(CardData card, string[] row, string assetPath)
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
            skillCard.image = LoadCardSprite(card.spriteName);

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

        private static Sprite LoadCardSprite(string spriteName)
        {
            // Add extension automatically if not included
            if (!spriteName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                spriteName += ".png";

            // Path relative to project root
            string path = $"Assets/Art/Cards/{spriteName}";

            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            return sprite;
        }

    } 
}
