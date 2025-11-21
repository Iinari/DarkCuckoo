using UnityEngine;
using UnityEditor;
using System.Net;
using System.IO;
using Unity.VisualScripting;
using SnIProductions;
using System;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Security.Cryptography;



//Creates a tool for importing data from Google Sheet
public class GoogleSheetImporter : Editor
{
    private const string masterSheetUrl = "https://docs.google.com/spreadsheets/d/e/2PACX-1vROZLeQBhzRp9K0pAJvUClGkAwMZ0PdCMq7yC8uJ2WDXbWnSdBzZUCUkF2oCxOm2l3VbzOlX-Td0spY/pub?output=csv";
    private const string attackSheetUrl =
    "https://docs.google.com/spreadsheets/d/1wtEEX3_Z8NZSxDyamQn9buS0e_k-vbSEhwLAyzMib4Q/gviz/tq?tqx=out:csv&gid=294967941";

    private const string skillSheetUrl =
        "https://docs.google.com/spreadsheets/d/1wtEEX3_Z8NZSxDyamQn9buS0e_k-vbSEhwLAyzMib4Q/gviz/tq?tqx=out:csv&gid=184911254";

    private const string savePath = "Assets/Resources/ImportedCards/";


    [MenuItem("Tools/Import/Import Google Sheet Card Data")]

    

    //Imports Google Sheet when menu item created above is clicked
    public static void ImportSheet()
    {
        Dictionary<int, string[]> attackCardsDic = new();
        Dictionary<int, string[]> skillCardsDic = new();

        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        string masterCSV = DownloadCSV(masterSheetUrl);
        string attackCSV = DownloadCSV(attackSheetUrl);
        string skillCSV = DownloadCSV(skillSheetUrl);


        var masterRows = GoogleSheetParser.ParseSheet(masterCSV);

        attackCardsDic = GoogleSheetParser.ParseSubCSVToDictionary(attackCSV);
        skillCardsDic = GoogleSheetParser.ParseSubCSVToDictionary(skillCSV);

        string[] lines = masterCSV.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

        // Skip header (header row 0 is used as headline, not for data)
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            string[] columns = line.Split(',');

            // Create scriptable object 
            CardData card = ScriptableObject.CreateInstance<CardData>();

            card.ID = GoogleSheetParser.ParseInt(columns[0]);

            if (card.ID == 0)
            {
                Debug.LogWarning("Can't create card asset with ID value 0.");
            }
            else
            {
                string name = columns[1];
                CardType type = GoogleSheetParser.ParseCardType(columns[2]);
                bool isInStartDeck = GoogleSheetParser.ParseBool(columns[3]);
                int cost = GoogleSheetParser.ParseInt(columns[4]);
                Rarity rarity = GoogleSheetParser.ParseRarity(columns[5]);
                string sprite = columns[6];
                string desc = columns[7];

                card.cardName = name;
                card.cardType = type;
                card.isInStartingDeck = isInStartDeck;
                card.cost = cost;
                card.cardRarity = rarity;
                card.spriteName = sprite;
                card.cardDescription = desc;

                switch (type)
                {
                    case CardType.Attack:

                        if (attackCardsDic.TryGetValue(card.ID, out var attackRow))
                        {
                            //Debug.Log(card.ID + " damage: " + attackRow[8]);
                        }
                        else
                        {
                            Debug.LogWarning($"No attack data for card ID {card.ID}");
                        }
                        //FillAttackData(card, attackRows[card.ID]);
                        break;

                    case CardType.Skill:
                        ImporterUtility.CreateCardAsset(card, savePath);
                        //card = ScriptableObject.CreateInstance<SkillCardData>();
                        //FillSkillData((SkillCardData)card, skillRows[card.ID]);
                        break;

                    default:
                        card = ScriptableObject.CreateInstance<CardData>();
                        break;
                }



                

                /*if (CardExists(card, savePath))
                {
                    UpdateValuesBasedOnType(card);
                }
                else
                {
                    CreateCardBasedOnType(card);
                }*/

            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Google Sheet Import Complete!");
    }

    private static void FillSkillData(SkillCardData card, string[] strings)
    {
        throw new NotImplementedException();
    }

    private static void FillAttackData(CardData card, string[] row)
    {
        AttackCardData attackCard = ScriptableObject.CreateInstance<AttackCardData>();
        attackCard.cardName = card.cardName;
        attackCard.cardType = card.cardType;
        attackCard.isInStartingDeck = card.isInStartingDeck;
        attackCard.cost = card.cost;
        attackCard.cardRarity = card.cardRarity;
        attackCard.spriteName = card.spriteName;
        attackCard.cardDescription = card.cardDescription;
        attackCard.damage = GoogleSheetParser.ParseInt(row[8]);
        Debug.Log(card.cardName + " damage: " + (row[8]));
    }

    private static string DownloadCSV(string url)
    {
        using (WebClient wc = new WebClient())
        {
            return wc.DownloadString(url);
        }
    }

    //Checks if card with the ID already exists in the folder
    private static bool CardExists(CardData card, string savePath)
    {
        string assetPath = $"{savePath}Card_{card.ID}.asset";
        CardData existingCard = AssetDatabase.LoadAssetAtPath<CardData>(assetPath);

        if (existingCard != null)
            return true;
        else
            return false;
    }



    private static void UpdateValuesBasedOnType(CardData card)
    {
        switch (card.cardType)
        {
            case CardType.Attack:
                //SetAttackCardValues(card);
                break;
            case CardType.Skill:
                break;
            default:
                break;
        }
    }

    private static void CreateCardBasedOnType(CardData card)
    {
        switch (card.cardType)
        {
            case CardType.Attack:
                card = ScriptableObject.CreateInstance<AttackCardData>();
                break;
            case CardType.Skill:
                break;
            default:
                break;
        }
    }
    /*
    public static void SetAttackCardValues(CardData card)
    {
        foreach (var row in attackRows)
        {

        }
        AttackCardData attackCard = ScriptableObject.CreateInstance<AttackCardData>();
        attackCard.cardName = card.cardName;
        attackCard.ID = card.ID;
        attackCard.cardType = card.cardType;
        attackCard.isInStartingDeck = card.isInStartingDeck;
        attackCard.cost = card.cost;
        attackCard.cardRarity = card.cardRarity;
        attackCard.spriteName = card.spriteName;
        attackCard.cardDescription = card.cardDescription;
        attackCard.damage = card.damage;

    }*/


    public static void CreateAttackCard(CardData card, string savePath)
    {

    }

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


    public static CardData GetCardType(CardData card)
    {
        switch (card.cardType)
        {
            case CardType.Attack:

                return card;

            case CardType.Skill:
                CardData skillCard = ScriptableObject.CreateInstance<SkillCardData>();
                return skillCard;
            default:
                return card;
        }
    }

}
