using System.Collections.Generic;
using UnityEngine;


//Includes Enums and generally useful functions
namespace SnIProductions
{
    public static class Utility
    {
        public static void Shuffle<T>(List<T> list)
        {
            System.Random random = new();
            int n = list.Count;

            for (int i = n - 1; i > 0; i--) { 
                int j = random.Next(i +1);
                (list[j], list[i]) = (list[i], list[j]);
            }
        }

        public static bool ProbabilityCheck(int probability)
        {
            int rnd = Random.Range(0, 101);
            return rnd <= probability;
        }
    }

    public enum Rarity
    {
        Common,

        Rare,

        Epic,

        Legendary
    }

    public enum CardType
    {
        Attack,

        Skill,

        Power,

        Minion
    }

    public enum HeroClass
    {
        None,

        Exiled,

        Juustoukko
    }

    public enum DamageType
    {
        Physical,

        Magic
    }

    public enum DamageSubType
    {
        None,

        Fire,

        Arcane,

        Shadow
    }

    public enum EnemyActions
    {
        Attack,

        Heal,

        Debuff,

        Buff,

        Defend,

        Summon
    }

    public enum CardState
    {
        Default,

        Hovered,

        Dragging,

        Targeting,

        Playing
    }
}
