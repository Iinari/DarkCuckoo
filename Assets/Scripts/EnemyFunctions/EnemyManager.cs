using SnIProductions;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//This class is ment to manage enemy/enemies during fight. This is needed if encounter has more than one enemy
//as enemies only know about themselves as units
public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemyList;
    public void DisplayEnemy(EnemyData enemyData, GameObject enemyPrefab, Transform enemyTransform)
    {
        //Instatiate the enemy
        GameObject newEnemy = Instantiate(enemyPrefab, enemyTransform.position, Quaternion.identity, enemyTransform);

        newEnemy.GetComponent<SpriteRenderer>().sprite = enemyData.enemySprite;

        Vector2 S = newEnemy.GetComponent<SpriteRenderer>().sprite.bounds.size;
        newEnemy.GetComponent<BoxCollider2D>().size= S;

        //Set the Enemys data of the instantieted enemy
        newEnemy.GetComponent<Enemy>().enemyData = enemyData;
        newEnemy.GetComponent<Enemy>().UpdateEnemyDisplay();

        enemyList.Add(newEnemy.GetComponent<Enemy>());
     
    }
}
