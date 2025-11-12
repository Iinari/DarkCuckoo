using SnIProductions;
using UnityEngine;

[CreateAssetMenu(fileName = "Hero", menuName = "Scriptable Objects/HeroData")]
public class HeroData : UnitData
{
    public HeroClass HeroClass;

    public string heroName;

    public Sprite heroSprite;

    public Sprite manaPoolIcon;

    public int health;

    public int mana;

    public float strength;

    public float luck;

}
