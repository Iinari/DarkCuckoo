using Newtonsoft.Json;
using SnIProductions;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;


public class CardTypeLocalizer : CardLocalizer
{
    public override void ConstructKey(CardData cardData)
    {
        string key = "type." + cardData.type.ToString();
        SetKey(key);

        localizedString.RefreshString();
    }
}
