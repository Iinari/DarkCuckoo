using Unity.VisualScripting;
using UnityEngine;
using SnIProductions;

public class CardPlayManager : MonoBehaviour
{
    public Card playedCard;

    public Enemy lastTarget;

    public Hero playerHero;

    private HandManager handManager;

    private DiscardManager discardManager;

    void Awake()
    {
        handManager = FindFirstObjectByType<HandManager>();
        discardManager = FindFirstObjectByType<DiscardManager>();
    }

    public Enemy GetTargetEnemy()
    {
        return lastTarget;
    }

    public void SetTargetEnemy(Enemy target)
    {
        if (target != null)
        {
            lastTarget = target;
        }

    }

    public Card GetLastPlayedCard()
    {
        return playedCard;
    }

    public void SetLastPlayedCard(Card card)
    {
        if (card != null)
        {
            playedCard = card;  
        }
    }

    public void PlayTheCard(GameObject cardObj)
    {   
        SetLastPlayedCard(cardObj.GetComponent<Card>());

        if (playedCard != null)
        {
            switch (playedCard.cardData.type)
            {
                case CardType.Attack:
                    if (lastTarget != null)
                    {
                        lastTarget.TakeDamage(playedCard.cardData.GetDamage());
                    }
                    else Debug.Log(playedCard.cardData.type + "didn't have valid target");
                    break;
                case CardType.Skill:
                    PlaySkillCard(playedCard);
                    break;
            }
            DecreaseMP(playedCard.manaCost);

            DiscardThisCard();
            handManager.UnregisterCard(cardObj);
        }    
    }


    public bool CheckHasEnoughMana(int cardCost)
    { 
        if (playerHero.attributesManager.mp - cardCost >= 0)
        {
            return (playerHero.attributesManager.mp - cardCost >= 0);
        }
        else
        {
            Debug.Log("NOT ENOUGH MANA");
            return (playerHero.attributesManager.mp - cardCost >= 0);
        }
        
    }

    public void DecreaseMP(int mpAmount)
    {
        playerHero.GetComponent<AttributesManager>().ModifyAttribute(AttributesManager.Attribute.MP, -mpAmount);
    }

    public void PlaySkillCard(Card card)
    {
        
        int heal = card.cardData.GetHealPower();
        if (heal > 0) 
        {
            playerHero.GetComponent<AttributesManager>().ModifyAttribute(AttributesManager.Attribute.HP, heal);
        }

        int block = card.cardData.GetBlockPower();
        if (block > 0)
        {
            Debug.Log("Gained " + block + " block");
        }

    }
    
    public void DiscardThisCard()
    {
        if (discardManager != null)
        {
            discardManager.AddToDiscard(playedCard.cardData);
            //Destroy(gameObject);
        }
    }

    public void TargetEnemyWithPlay(Enemy enemy, GameObject cardObj)
    {

        if (handManager == null)
        {
            handManager = FindFirstObjectByType<HandManager>();
        }
        if (handManager != null)
        {
            if (discardManager == null)
            {
                discardManager = FindFirstObjectByType<DiscardManager>();
            }
            
            SetLastPlayedCard(playedCard);
            SetTargetEnemy(enemy);
            PlayTheCard(cardObj);

        }
    }


}
