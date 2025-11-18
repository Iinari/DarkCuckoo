using UnityEngine;
using UnityEditor;
using System.Net;
using System.IO;
using Unity.VisualScripting;
using SnIProductions;
using System;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;

//Creates a tool for importing data from Google Sheet
public class GoogleSheetImporter : Editor
{
    private const string sheetUrl = "https://docs.google.com/spreadsheets/d/e/2PACX-1vROZLeQBhzRp9K0pAJvUClGkAwMZ0PdCMq7yC8uJ2WDXbWnSdBzZUCUkF2oCxOm2l3VbzOlX-Td0spY/pub?output=csv";
    private const string savePath = "Assets/Resources/ImportedCards/";

    [MenuItem("Tools/Import/Import Google Sheet")]

    //Imports Google Sheet when menu item created above is clicked
    public static void ImportSheet()
    {
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        string csv = DownloadCSV(sheetUrl);
        string[] lines = csv.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

        // Skip header (header row 0 is used as headline, not for data)
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            string[] columns = line.Split(',');

            // Create scriptable object 
            CardData card = ScriptableObject.CreateInstance<CardData>();

            card.ID = ParseInt(columns[0]);

            if (card.ID == 0)
            {
                Debug.LogWarning("Can't create asset with ID value 0.");
            }
            else
            {
                card.cardName = columns[1];
                card.cardType = ParseCardType(columns[2]);
                card.isInStartingDeck = ParseBool(columns[3]);
                card.cost = ParseInt(columns[4]);
                card.cardRarity = ParseRarity(columns[5]);
                card.spriteName = columns[6];
                card.cardDescription = columns[7];

                CreateCardAsset(card);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Google Sheet Import Complete!");
    }

    private static void CreateCardAsset(CardData card)
    {
        string assetPath = $"{savePath}Card_{card.ID}.asset";

        CardData existingCard = AssetDatabase.LoadAssetAtPath<CardData>(assetPath);

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
            AssetDatabase.CreateAsset(card, assetPath);
        }
    }


    private static string DownloadCSV(string url)
    {
        using (WebClient wc = new WebClient())
        {
            return wc.DownloadString(url);
        }
    }

    private static bool ParseBool(string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        value = value.Trim().ToLower();

        return value == "true" || value == "1" || value == "yes";
    }

    private static CardType ParseCardType(string value)
    {
        value = value.Trim();

        if (Enum.TryParse<CardType>(value, ignoreCase: true, out var result))
            return result;

        Debug.LogWarning($"CardType '{value}' is invalid. Defaulting to Attack.");
        return CardType.Attack; // or pick your own fallback
    }

    private static Rarity ParseRarity(string value)
    {
        value = value.Trim();

        if (Enum.TryParse<Rarity>(value, ignoreCase: true, out var result))
            return result;

        Debug.LogWarning($"CardType '{value}' is invalid. Defaulting to Attack.");
        return Rarity.Common; // or pick your own fallback
    }

    private static int ParseInt(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Debug.LogWarning("Tried to parse an empty int field. Returning 0.");
            return 0;
        }

        // Remove invisible Unicode characters + trim spaces
        value = value.Trim().Replace("\uFEFF", "");  // Remove BOM if present

        if (int.TryParse(value, out int result))
            return result;

        Debug.LogError($"Failed to parse int from '{value}'");
        return 0;
    }

}
