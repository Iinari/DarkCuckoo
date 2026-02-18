using Newtonsoft.Json;
using SnIProductions;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public abstract class CardLocalizer : MonoBehaviour
{
    [SerializeField] protected LocalizedString localizedString;
    [SerializeField] protected TMP_Text txtUI;

    public string TranslatedValue { get; private set; }

    protected virtual void OnEnable()
    {
        localizedString.StringChanged += ValueChanged;
    }

    protected virtual void OnDisable()
    {
        localizedString.StringChanged -= ValueChanged;
    }

    void ValueChanged(string value)
    {
        TranslatedValue = value;
        txtUI.text = value;
    }

    public abstract void ConstructKey(CardData cardData);

    protected void SetKey(string key)
    {
        localizedString.TableEntryReference = key;
    }
}
