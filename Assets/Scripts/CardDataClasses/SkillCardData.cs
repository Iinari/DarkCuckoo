using SnIProductions;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillCard", menuName = "Cards/SkillCard")]
public class SkillCardData : CardData
{

    public int healPower;
    public int blockPower;

    public override void UpdateCardDescription()
    {
        string healAsString = healPower.ToString();
        cardDescription = cardDescription.Replace("#", healAsString);
        Debug.Log(cardDescription);
    }

    public override int GetHealPower()
    {
        return healPower;
    }

    public override int GetBlockPower() { return blockPower; }

    public override string GetCardDescription()
    {
        string healAsString = healPower.ToString();
        string tempDescpription = cardDescription.Replace("#", healAsString);
        return tempDescpription;
    }
}
