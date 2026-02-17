using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class CardStringLocalizer : MonoBehaviour
{
    public LocalizedString localizedString;

    public string keyWithoutID;

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

    public void ConstructKey(int cardID)
    {
        stringTableKey = cardID.ToString() + keyWithoutID;
        localizedString = new LocalizedString(localizedString.TableReference, stringTableKey); 
    }
}
