using SnIProductions;
using UnityEngine;

[CreateAssetMenu(fileName = "MinionCard", menuName = "Cards/MinionCard")]
public class MinionCardData : CardData
{
    public int health;

    public int damageMin;
    public int damageMax;

    public DamageSubType damageSubType;
    public DamageType damageType;


    public override void UpdateCardDescription()
    {
        base.UpdateCardDescription();
    }
}
