using SnIProductions;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class CardDescrLocalizer : CardStringLocalizer
{
    private int currentValue;

    private CardArgs args = new CardArgs();

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
        txtUI.text = value;
    }

    public override void ConstructKey(CardData cardData)
    {
        string key = cardData.ID.ToString() + keyWithoutID;
        localizedString.TableEntryReference = key;

        DescriptionLocalization(cardData);
    }

    public void SetValue(int value)
    {
        currentValue = value;
        Refresh();
    }

    void Refresh()
    {
        args.amount = currentValue;

        localizedString.Arguments = new object[] { args };
        localizedString.RefreshString();
    }

    public void DescriptionLocalization(CardData cardData)
    {
        switch (cardData.type)
        {
            case CardType.Attack:
                SetValue(cardData.GetDamage());
                break;
            case CardType.Skill:
                SetValue(cardData.GetHealPower());
                break;

        }
    }
}
