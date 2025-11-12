using SnIProductions;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackCard", menuName = "Cards/AttackCard")]
public class AttackCardData : CardData
{
    public int damage;

    public int damageMin;
    public int damageMax;

    public DamageType damageType;
    public DamageSubType damageSubType;


    public override int GetDamage()
    {
        return damage;
    }

    public override string GetCardDescription()
    {
        string dmgAsString = damage.ToString();
        string tempDescription = cardDescription.Replace("#", dmgAsString);
        return tempDescription;
    }

    public override void UpdateCardDescription()
    {
        string dmgAsString = damage.ToString();
        cardDescription = cardDescription.Replace("#", dmgAsString);
    }
}
