using SnIProductions;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    //public GameObject heroPrefab; //Assign hero prefab in inspector

    //public Transform heroTransform; //Root of the hero position

    public Hero PlayerHero { get; private set; }

    public void DisplayHero(HeroData heroData, GameObject heroPrefab, Transform heroTransform)
    {
        //Instatiate the hero
        GameObject newHero = Instantiate(heroPrefab, heroTransform.position, Quaternion.identity, heroTransform);

        if(newHero.GetComponent<SpriteRenderer>() == null)
        {
            Debug.Log("instantiated hero didn't have SpriteRenderer component");
        }
        else
        {
            newHero.GetComponent<SpriteRenderer>().sprite = heroData.heroSprite;

        }



        if (newHero.GetComponent<Hero>() == null)
        {
            Debug.Log("instantiated object didn't have Hero component");
        }
        else 
        {
            newHero.GetComponent<Hero>().heroData = heroData;
            newHero.GetComponent<Hero>().FirstUpdateHeroDisplay();
            PlayerHero = newHero.GetComponent<Hero>();
        }
    }
}
