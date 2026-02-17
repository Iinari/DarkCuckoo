using Newtonsoft.Json;
using SnIProductions;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class CardTypeLocalizer : CardStringLocalizer
{
    private string stringTableKey;


    public override void ConstructKey(CardData cardData)
    {
        stringTableKey = "type." + cardData.type.ToString();
        localizedString = new LocalizedString(localizedString.TableReference, stringTableKey);
    }
      
}
