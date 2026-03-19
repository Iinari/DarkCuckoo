using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Encounters/Enemy")]


public class EnemyData : EncounterData
{
    public int enemyID;

    public Sprite enemySprite;

    public float scale;

    public string enemyName;

    public float enemyHealth;

    public int enemyMinDmg;
    public int enemyMaxDmg;

}
