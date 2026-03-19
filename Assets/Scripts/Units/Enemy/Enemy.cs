using UnityEngine.UI;
using TMPro;
using UnityEngine;
using SnIProductions;

public class Enemy : BattleUnit, IDataPersistence
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

    public SpriteRenderer spriteRenderer;

    public BoxCollider2D boxCollider;

    private BattleStateStatus battleStateStatus;

    private Hero player;
    private void OnEnable()
    {
        DataPersistenceManager.Instance.RegisterDataPersistenceObject(this);
    }

    private void OnDisable()
    {
        DataPersistenceManager.Instance.UnregisterDataPersistenceObject(this);
    }

    public void LoadData(GameData data)
    {
        data.enemyHP = enemyCurrentHealth;
        UpdateVisuals();    
    }

    public void SaveData(ref GameData data)
    {
        enemyCurrentHealth = data.enemyHP;
    }

    public void ResetToDefault(ref GameData data)
    {
        //TO-DO implement enemies have whole Stat System
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
            UpdateVisuals();
        }
        if (enemyCurrentHealth <= 0)
        {
            BattleContext.Instance.battleManager.EndBattle(BattleResult.Won);
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
            player = BattleContext.Instance.playerHero;
            player.GetComponent<UnitHealthWatch>().TakeDamage(-dmg);
        }

    }

    public void UpdateVisuals()
    {
        enemyHPTxt.text = enemyCurrentHealth.ToString();
        hpSlider.value = enemyCurrentHealth;
    }
}
