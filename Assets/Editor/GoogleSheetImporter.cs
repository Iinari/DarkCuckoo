using UnityEngine;
using UnityEditor;
using System.Net;
using System.IO;
using Unity.VisualScripting;
using SnIProductions;
using System;
using UnityEngine.UIElements;

public class GoogleSheetImporter : Editor
{
    private const string sheetUrl = "https://docs.google.com/spreadsheets/d/e/2PACX-1vROZLeQBhzRp9K0pAJvUClGkAwMZ0PdCMq7yC8uJ2WDXbWnSdBzZUCUkF2oCxOm2l3VbzOlX-Td0spY/pub?output=csv";
    private const string savePath = "Assets/Resources/ImportedCards/";

    [MenuItem("Tools/Import/Import Google Sheet")]

    public static void ImportSheet()
    {
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        string csv = DownloadCSV(sheetUrl);
        string[] lines = csv.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

        // Skip header
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            // Skip totally blank rows
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] columns = line.Split(',');

            // Skip rows where ALL columns are empty
            bool allEmpty = true;
            foreach (string col in columns)
            {
                if (!string.IsNullOrWhiteSpace(col))
                {
                    allEmpty = false;
                    break;
                }
            }
            if (allEmpty)
                continue;

            // Now you know it's a real row with some data
            CardData card = ScriptableObject.CreateInstance<CardData>();

            card.ID = ParseInteger(columns[0]);
            card.cardName = columns[1];
            card.cardType = ParseCardType(columns[2]);
            card.isInStartingDeck = ParseBool(columns[3]);
            card.cost = ParseInteger(columns[4]);
            card.cardRarity = ParseRarity(columns[5]);
            card.spriteName = columns[6];
            card.cardDescription = columns[7];

            string assetPath = $"{savePath}Card_{card.ID}.asset";
            AssetDatabase.CreateAsset(card, assetPath);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Google Sheet Import Complete!");
    }

    private static int ParseInt(string v)
    {
        throw new NotImplementedException();
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

    private static int ParseInteger(string value)
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
