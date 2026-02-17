using Newtonsoft.Json;
using SnIProductions;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class CardStringLocalizer : MonoBehaviour
{
    public LocalizedString localizedString;

    public string keyWithoutID;
    public TMP_Text txtUI;

    public string TranslatedValue { get; private set; }

    void OnEnable()
    {
        localizedString.StringChanged += ValueChanged;
    }

    void OnDisable()
    {
        localizedString.StringChanged -= ValueChanged;
    }

    void ValueChanged(string value)
    {
        TranslatedValue = value;
        txtUI.text = value;
    }

    public virtual void ConstructKey(CardData cardData)
    {
        string key = cardData.ID.ToString() + keyWithoutID;
        localizedString.TableEntryReference = key;

        Refresh();
    }

    void Refresh()
    {
        localizedString.RefreshString();
    }

}
