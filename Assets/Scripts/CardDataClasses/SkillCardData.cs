using SnIProductions;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillCard", menuName = "Cards/SkillCard")]
public class SkillCardData : CardData
{

    public int heal;
    public int block;

    public override void UpdateCardDescription()
    {
        string healAsString = heal.ToString();
        description = description.Replace("#", healAsString);
        Debug.Log(description);
    }

    public override int GetHealPower()
    {
        return heal;
    }

    public override int GetBlockPower() { return block; }

    public override string GetCardDescription()
    {
        string healAsString = heal.ToString();
        string tempDescpription = description.Replace("#", healAsString);
        return tempDescpription;
    }
}
