using UnityEngine;
using SnIProductions;
using System.Collections.Generic;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class AttributesManager : MonoBehaviour
{
    //If new attribute is implemented it should be added to enum
    public enum Attribute
    {
        Health,

        HP,

        Mana,

        MP,

        Strength,

        Luck,

        Satiety
    }

    //Attributes
    public int fullHealth = 0;
    public int hp = 0;
    public int fullMana = 0;
    public int mp = 0;
    public float strength = 0;
    public float luck = 0;
    public float satiety = 0;

    //Max values for attributes
    private readonly int maxHealth = 100000;
    private readonly int maxMana = 100;
    private readonly float maxStrengtht = 200f;
    private readonly float maxLuck = 100f;
    private readonly float maxSatiety = 100f;

    //Min values for attributes
    private readonly int minMaxHealth = 1;
    private readonly int minHp = 0;
    private readonly int minMana = 0;
    private readonly float minStrength = 0;
    private readonly float minLuck = 0;
    private readonly float minSatiety = 0;

    //Stored value
    public int s_Health;
    public int s_hp;
    public int s_Mana;
    public int s_mp;
    public float s_Strength;
    public float s_Luck;
    public float s_Satiety;

    public HeroData playerHeroData = null;

    public void ModifyAttribute(Attribute attribute, float value)
    {
        switch (attribute) 
        { 
            case Attribute.Health:
                fullHealth = Mathf.Clamp(maxHealth + (int)value, minMaxHealth, maxHealth);
                break;

            case Attribute.HP:
                hp = Mathf.Clamp(hp + (int)value, minHp, fullHealth);
                break;

            case Attribute.Mana:
                fullMana = Mathf.Clamp(maxMana + (int)value, minMana, maxMana); 
                break;

            case Attribute.MP:
                mp = Mathf.Clamp(mp + (int)value, minMana, fullMana);
                break;

            case Attribute.Strength:
                strength = Mathf.Clamp(strength + (int)value, minStrength,maxStrengtht);
                break;

            case Attribute.Luck:
                luck = Mathf.Clamp(luck + (int)value, minLuck, maxLuck);
                break;
            case Attribute.Satiety:
                satiety = Mathf.Clamp(satiety + (int)value, minSatiety, maxSatiety);
                break;
            default:
                Debug.LogWarning($"Invalid attribute passed to ModifyAttribute: {attribute}");
                break;
        }

    }

    public void RestoreAttribute(Attribute attribute)
    {
        switch (attribute)
        {
            case Attribute.Health:
                fullHealth = s_Health;
                break;
            case Attribute.HP:
                hp = fullHealth;
                break;
            case Attribute.Mana:
                fullMana = s_Mana;
                break;
            case Attribute.MP:
                mp = fullMana;
                break;
            case Attribute.Strength:
                strength = s_Strength;
                break;
            case Attribute.Luck:
                luck = s_Luck;
                break;
            case Attribute.Satiety:
                satiety = s_Satiety;
                break;
            default:
                Debug.LogWarning($"Invalid attribute passed to RestoreAttribute: {attribute}");
                break;
        }
    }

    public void RestoreAllAttributes() 
    {
        fullHealth = s_Health;
        //hp = fullHealth;
        fullMana = s_Mana;
        //mp = fullMana;
        strength = s_Strength;
        luck = s_Luck;
        satiety = s_Satiety;

        Debug.Log("Restored all");
    }

    public void LoadPlayerHeroData(HeroData DataToLoad)
    {
        if (DataToLoad == null)
        {
            Debug.LogWarning("HeroData to load is null");
        }
        else
        {
            playerHeroData = DataToLoad;

            fullHealth = DataToLoad.health;
            hp = fullHealth;
            fullMana = DataToLoad.mana;
            mp = fullMana;
            strength = DataToLoad.strength;
            luck = DataToLoad.luck;
            satiety = DataToLoad.satiety;

            StoreAllAttributes();
        }
    }

    public void StoreAttribute(Attribute attribute)
    {
        switch (attribute)
        {
            case Attribute.Health:
                s_Health = fullHealth;
                break;
            case Attribute.HP:
                s_hp = hp;
                break;
            case Attribute.Mana:
                s_Mana = fullMana;
                break;
            case Attribute.MP:
                s_mp = mp;
                break;
            case Attribute.Strength:
                s_Strength = strength;
                break;
            case Attribute.Luck:
                s_Luck = luck;
                break;
            case Attribute.Satiety:
                s_Satiety = satiety;
                break;
            default:
                Debug.LogWarning($"Invalid attribute passed to StoreAttribute: {attribute}");
                break;
        }

    }

    public void StoreAllAttributes()
    {
        s_Health = fullHealth;
        s_hp = hp;
        s_Mana = fullMana;
        s_mp = mp;
        s_Strength = strength;
        s_Luck = luck;
        s_Satiety = satiety;
    }

}
