using SnIProductions;
using System.Collections.Generic;
using System;
using UnityEditor.Overlays;
using UnityEngine;
using System.IO;


public class GoogleSheetParser
{
    public static int ParseInt(string value)
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


    public static Rarity ParseRarity(string value)
    {
        value = value.Trim();

        if (Enum.TryParse<Rarity>(value, ignoreCase: true, out var result))
            return result;

        Debug.LogWarning($"CardType '{value}' is invalid. Defaulting to Attack.");
        return Rarity.Common;
    }


    public static CardType ParseCardType(string value)
    {
        value = value.Trim();

        if (Enum.TryParse<CardType>(value, ignoreCase: true, out var result))
            return result;

        Debug.LogWarning($"CardType '{value}' is invalid. Defaulting to Attack.");
        return CardType.Attack;
    }
    public static bool ParseBool(string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        value = value.Trim().ToLower();

        return value == "true" || value == "1" || value == "yes";
    }

    public static Dictionary<int, string[]> ParseSubCSVToDictionary(string csv)
    {
        var dict = new Dictionary<int, string[]>();
        bool insideQuote = false;

        bool isFirstRow = true;   // <-- NEW

        List<string> currentRow = new();
        string currentValue = "";

        void CommitRow()
        {
            if (currentValue.Length > 0 || currentRow.Count > 0)
            {
                currentRow.Add(currentValue);
                currentValue = "";

                var rowArray = currentRow.ToArray();
                currentRow.Clear();

                // Skip header row
                if (isFirstRow)
                {
                    isFirstRow = false;
                    return;
                }

                // No columns? Skip.
                if (rowArray.Length == 0 || string.IsNullOrWhiteSpace(rowArray[0]))
                    return;

                // Parse ID
                if (!int.TryParse(rowArray[0].Trim(), out int id))
                {
                    Debug.LogWarning($"[ParseSubCSV] Row has invalid ID: '{rowArray[0]}'");
                    return;
                }

                if (dict.ContainsKey(id))
                {
                    Debug.LogWarning($"[ParseSubCSV] Duplicate ID found: {id}");
                    return;
                }

                dict.Add(id, rowArray);
            }
        }

        for (int i = 0; i < csv.Length; i++)
        {
            char c = csv[i];

            if (c == '"')
            {
                if (insideQuote && i + 1 < csv.Length && csv[i + 1] == '"')
                {
                    currentValue += '"';
                    i++;
                }
                else
                {
                    insideQuote = !insideQuote;
                }
            }
            else if (c == ',' && !insideQuote)
            {
                currentRow.Add(currentValue);
                currentValue = "";
            }
            else if ((c == '\n' || c == '\r') && !insideQuote)
            {
                CommitRow();
            }
            else
            {
                currentValue += c;
            }
        }

        CommitRow(); // Final row

        return dict;
    }


}
