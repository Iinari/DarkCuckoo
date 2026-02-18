using Newtonsoft.Json;
using SnIProductions;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class CardStringLocalizer : CardLocalizer
{
    public string keyString;
    public override void ConstructKey(CardData cardData)
    {
        string key = cardData.ID + keyString;
        SetKey(key);

        localizedString.RefreshString();
    }
}
