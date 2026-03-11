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

    public AttributesManager attributes;

    void OnEnable()
    {
        attributes.OnStatChanged += UpdateStatUI;
    }

    void OnDisable()
    {
        attributes.OnStatChanged -= UpdateStatUI;
    }

    void UpdateStatUI(StatType type, Stat stat)
    {
        switch (type)
        {
            case StatType.Health:
                heroHPTxt.text = stat.current.ToString();
                float hpProsent = stat.current / stat.max;
                roundHpBar.fillAmount = hpProsent;
                break;

            case StatType.Mana:
                heroMPTxt.text = stat.current.ToString();
                float mpProsent = stat.current / stat.max;
                manaPoolIcon.fillAmount = mpProsent;
                break;
        }
    }

    private void Awake()
    {
        cardPlayManager = FindFirstObjectByType<CardPlayManager>();
        cardPlayManager.playerHero = this;

        attributes = GetComponent<AttributesManager>();
    }

    public void SetHeroData(HeroData newHeroData)
    {
        heroData = newHeroData;
        
        /*heroCurrentHealth = heroData.health;
        heroHPTxt.text = heroData.health.ToString();

        heroMPTxt.text = heroData.mana.ToString();
        heroMP = heroData.mana;*/

        manaPoolIcon.sprite = heroData.manaPoolIcon;

        //roundHpBar.fillAmount = attributes.hp;

        //attributes.LoadPlayerHeroData(heroData);
    }
   

    public void UpdateVisuals(StatType type, Stat stat)
    {
        switch (type)
        {
            case StatType.Health:
                heroHPTxt.text = stat.current.ToString();
                float hpProsent = stat.current / stat.max;
                roundHpBar.fillAmount = hpProsent;
                break;

            case StatType.Mana:
                heroMPTxt.text = stat.current.ToString();
                float mpProsent = stat.current / stat.max;
                manaPoolIcon.fillAmount = mpProsent;
                break;
        }
    }
}
