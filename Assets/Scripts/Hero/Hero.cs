using UnityEngine.UI;
using TMPro;
using UnityEngine;
using SnIProductions;

public class Hero : BattleUnit
{
    public HeroData heroData;

    public TMP_Text heroNameTxt;

    public Sprite heroImage;

    public TMP_Text heroHPTxt;

    public float heroCurrentHealth;

    public int heroMP;

    public TMP_Text heroMPTxt;

    public Slider hpSlider;

    public Image manaPoolIcon;

    public Image roundHpBar;

    public CardPlayManager cardPlayManager;

    public AttributesManager attributesManager;

    private BattleSystem battleSystem;

    private void Update()
    {
        heroMPTxt.text = attributesManager.mp.ToString();
        heroHPTxt.text = attributesManager.hp.ToString();

        float hpProsent = attributesManager.hp / attributesManager.fullHealth;
        roundHpBar.fillAmount = hpProsent;
    }


    private void Awake()
    {
        cardPlayManager = FindFirstObjectByType<CardPlayManager>();
        cardPlayManager.playerHero = this;
        attributesManager = FindFirstObjectByType<AttributesManager>();
        battleSystem = FindFirstObjectByType<BattleSystem>();
        battleSystem.playerHero = this;
    }

    public void HeroDisplayFirstUpdate()
    {
        heroNameTxt.text = heroData.heroName;
        heroCurrentHealth = heroData.health;
        heroHPTxt.text = heroData.health.ToString();

        heroMPTxt.text = heroData.mana.ToString();
        heroMP = heroData.mana;

        manaPoolIcon.sprite = heroData.manaPoolIcon;

        roundHpBar.fillAmount = attributesManager.hp;

        attributesManager.LoadPlayerHeroData(heroData);
    }

    public void TakeDamage(float damage)
    {
        if (heroCurrentHealth > 0)
        {
            heroCurrentHealth -= damage;
            heroHPTxt.text = heroCurrentHealth.ToString();
            hpSlider.value = heroCurrentHealth;
        }
        if (heroCurrentHealth <= 0)
        {
            Debug.Log("Hero died");
        }
    }

    public void UpdateVisuals()
    {
        heroMPTxt.text = attributesManager.mp.ToString();
        heroHPTxt.text = attributesManager.hp.ToString();
        
        float hpProsent = attributesManager.hp / attributesManager.fullHealth;
        roundHpBar.fillAmount = hpProsent;
    }

}
