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
    private const string masterSheetUrl = 
        "https://docs.google.com/spreadsheets/d/e/2PACX-1vROZLeQBhzRp9K0pAJvUClGkAwMZ0PdCMq7yC8uJ2WDXbWnSdBzZUCUkF2oCxOm2l3VbzOlX-Td0spY/pub?output=csv";
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

        //MasterCardSheet is parsed to string[]
        var masterRows = GoogleSheetParser.ParseSheet(masterCSV);

        //Subsheets are parsed to dictrionaries <int, string[]>
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
                //Fill with data from MasterCardSheet
                card.cardName = columns[1];
                card.type = GoogleSheetParser.ParseCardType(columns[2]);
                card.isInStartDeck = GoogleSheetParser.ParseBool(columns[3]);
                card.cost = GoogleSheetParser.ParseInt(columns[4]);
                card.rarity = GoogleSheetParser.ParseRarity(columns[5]);
                card.spriteName = columns[6];
                card.description = columns[7];

                switch (card.type)
                {
                    case CardType.Attack:

                        if (attackCardsDic.TryGetValue(card.ID, out var attackRow))
                        {
                            ImporterUtility.FillAttackData(card, attackRow, savePath);
                        }
                        else
                        {
                            Debug.LogWarning($"No attack data for card ID {card.ID}");
                        }
                        break;

                    case CardType.Skill:
                        if (skillCardsDic.TryGetValue(card.ID, out var skillRow))
                        {
                            ImporterUtility.FillSkillData(card, skillRow, savePath);
                        }
                        else
                        {
                            Debug.LogWarning($"No skill data for card ID {card.ID}");
                        }
                        break;

                    default:
                        card = ScriptableObject.CreateInstance<CardData>();
                        break;
                }

            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Google Sheet Import Complete!");
    }

    private static string DownloadCSV(string url)
    {
        using (WebClient wc = new WebClient())
        {
            return wc.DownloadString(url);
        }
    }

}
