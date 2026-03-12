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

    void Start()
    {
        attributes = GameSession.Instance.AttributesManager;

        attributes.OnStatChanged += UpdateStatUI;

        Refresh();
    }

    void OnDestroy()
    {
        if (attributes != null)
            attributes.OnStatChanged -= UpdateStatUI;
    }

    void Refresh()
    {
        float hp = attributes.GetStat(StatType.Health);
        float hpMax = attributes.GetMaxStat(StatType.Health);

        if (hp > 0)
        {
            heroHPTxt.text = hp.ToString();
            roundHpBar.fillAmount = hp / hpMax;
        }

        float mp = attributes.GetStat(StatType.Mana);
        float mpMax = attributes.GetMaxStat(StatType.Mana);
        if (mp > 0)
        {
            heroMPTxt.text = mp.ToString();
            manaPoolIcon.fillAmount = mp / mpMax;
        }  
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

        attributes = GameSession.Instance.AttributesManager;
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
