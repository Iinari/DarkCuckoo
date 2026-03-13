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

    public BattleInitiator battleSystem;

    public SpriteRenderer spriteRenderer;

    public BoxCollider2D boxCollider;

    private BattleStateStatus battleStateStatus;

    private Hero player;

    private void Awake()
    {
        player = FindFirstObjectByType<Hero>();
    }

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
                battleSystem.popUpManager.OpenResultScreen(false);
                
            }
            else
            {
                battleSystem = FindFirstObjectByType<BattleInitiator>();
                battleSystem.popUpManager.OpenResultScreen(false);
                
            }
        }
    }

    public void TakeTurn()
    {
        int dmg = Random.Range(enemyData.enemyMinDmg, enemyData.enemyMaxDmg);

        if (player != null)
        {
            player.GetComponent<UnitHealthWatch>().TakeDamage(-dmg);
        }

        else
        {
            player = FindFirstObjectByType<Hero>();
            player.GetComponent<UnitHealthWatch>().TakeDamage(-dmg);
        }

    }
}
