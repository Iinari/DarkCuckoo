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
        // Instantiate the enemy prefab
        GameObject newEnemy = Instantiate(enemyPrefab, enemyTransform.position, Quaternion.identity, enemyTransform);

        Enemy enemyComponent = newEnemy.GetComponent<Enemy>();

        // Set the sprite
        enemyComponent.spriteRenderer.sprite = enemyData.enemySprite;

        // Adjust collider based on sprite size

        Vector2 S = enemyComponent.spriteRenderer.sprite.bounds.size;
        enemyComponent.boxCollider.size = S;

        /*Destroy(newEnemy.GetComponent<BoxCollider2D>());
        newEnemy.AddComponent<BoxCollider2D>();

        var col = newEnemy.GetComponent<BoxCollider2D>();
        Debug.Log("Collider center: " + col.offset + " size: " + col.size);*/

        // Assign enemy data
        enemyComponent.enemyData = enemyData;
        enemyComponent.UpdateEnemyDisplay();

        // Add to list
        enemyList.Add(enemyComponent);

    }
}
