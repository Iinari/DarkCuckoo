using Newtonsoft.Json;
using SnIProductions;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class CardTypeLocalizer : MonoBehaviour
{
    public LocalizedString localizedString;

    private string stringTableKey;

    public TMP_Text txtUI;

    public string TranslatedValue { get; private set; }

    void Start()
    {
        // Register to get an update when the string is changed.
        localizedString.StringChanged += ValueChanged;
    }

    void ValueChanged(string value)
    {
        TranslatedValue = value;
        txtUI.text = value;
    }

    public void ConstructTypeKey(CardType type)
    {
        stringTableKey = "type." + type.ToString();
        localizedString = new LocalizedString(localizedString.TableReference, stringTableKey);
    }
}
