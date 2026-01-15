using UnityEngine.UI;
using TMPro;
using UnityEngine;
using SnIProductions;

public class Enemy : BattleUnit
{
    public EnemyData enemyData;

    public TMP_Text enemyNameTxt;

    public ParticleSystem ParticleSystem;

    public Sprite enemyImage;

    public TMP_Text enemyHPTxt;

    public float enemyMaxHealth;

    public float enemyCurrentHealth;

    public Slider hpSlider;

    public Rarity enemyRarity;

    public BattleSystem battleSystem;

    public SpriteRenderer spriteRenderer;

    public BoxCollider2D boxCollider;


    public void UpdateEnemyDisplay()
    {
        enemyNameTxt.text = enemyData.enemyName;
        enemyMaxHealth = enemyData.enemyHealth;
        enemyCurrentHealth = enemyData.enemyHealth;
        enemyHPTxt.text = enemyData.enemyHealth.ToString();

        hpSlider.maxValue = enemyMaxHealth;
        hpSlider.value = enemyCurrentHealth;
    }

    public void TakeDamage(float damage)
    {
        if (enemyCurrentHealth > 0)
        {
            enemyCurrentHealth -= damage;
            enemyHPTxt.text = enemyCurrentHealth.ToString();
            hpSlider.value = enemyCurrentHealth;
        }
        if (enemyCurrentHealth <= 0)
        {
            if (battleSystem != null)
            {
                battleSystem.EnemyDied();
            }
            else
            {
                battleSystem = FindFirstObjectByType<BattleSystem>();
                battleSystem.EnemyDied();
            }
        }
    }

    public void TakeTurn(BattleSystem battleSystem)
    {
        int dmg = Random.Range(enemyData.enemyMinDmg, enemyData.enemyMaxDmg);
        battleSystem.TakeDamage(-dmg);
        battleSystem.PlayerTurn();
    }

}
