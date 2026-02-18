using SnIProductions;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class CardDescrLocalizer : CardLocalizer
{
    public string keyString;

    private int currentAmount;

    [System.Serializable]
    public class CardArgs
    {
        public int amount;
    }

    private CardArgs args = new();

    protected override void OnEnable()
    {
        base.OnEnable();

        args.amount = currentAmount;
        localizedString.Arguments = new object[] { args };
    }

    public override void ConstructKey(CardData cardData)
    {
        if (keyString != null) 
        {
            string key = cardData.ID + keyString;
            SetKey(key);
        }
        else
        {
            string key = cardData.ID + ".description";
            SetKey(key);
        }

        DescriptionLocalization(cardData);
    }

    public void SetAmount(int amount)
    {
        currentAmount = amount;
        Refresh();
    }

    void Refresh()
    {
        args.amount = currentAmount;
        localizedString.Arguments = new object[] { args };
        localizedString.RefreshString();
    }

    public void DescriptionLocalization(CardData cardData)
    {
        switch (cardData.type)
        {
            case CardType.Attack:
                SetAmount(cardData.GetDamage());
                break;
            case CardType.Skill:
                SetAmount(cardData.GetHealPower());
                break;

        }
    }
}