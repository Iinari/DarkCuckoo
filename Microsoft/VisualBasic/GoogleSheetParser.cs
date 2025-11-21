using SnIProductions;
using System.Collections.Generic;
using System;
using UnityEditor.Overlays;
using UnityEngine;
using System.IO;

namespace Microsoft.VisualBasic
public class GoogleSheetParser
{
    public static Dictionary<int, string[]> ParseSheet(string csv)
    {
        var dict = new Dictionary<int, string[]>();

        string[] lines = csv.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < lines.Length; i++)  // skip header row
        {

            Debug.Log(lines[i]);
            string line = lines[i].Trim();
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] cols = line.Split(',');

            // Skip entirely empty rows
            bool allEmpty = true;
            foreach (var c in cols)
                if (!string.IsNullOrWhiteSpace(c))
                    allEmpty = false;
            if (allEmpty) continue;

            int id = ParseInt(cols[0]);
            dict[id] = cols;
        }

        return dict;
    }

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

    private static List<string[]> ParseCSV(string csv)
    {
        List<string[]> rows = new();

        using (var reader = new StringReader(csv))
        using (var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(reader))
        {
            parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
            parser.SetDelimiters(",");

            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                if (fields != null)
                    rows.Add(fields);
            }
        }

        return rows;
    }

}
