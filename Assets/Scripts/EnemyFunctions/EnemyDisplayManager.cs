using SnIProductions;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

//This class is ment to display enemy/enemies during fight. This is needed if encounter has more than one enemy
//as enemies only know about themselves as units
public class EnemyDisplayManager : BattleComponent
{
    public List<Enemy> enemyReferences;

    public List<Transform> enemyPositions;

    private List<EnemyData> enemyDatas;

    public GameObject enemyPrefab;

    private EncounterEnemyTracker encounterEnemyTracker;

    private void Awake()
    {
        encounterEnemyTracker = GetComponent<EncounterEnemyTracker>();
    }

    public override void BattleSetUp()
    {
        encounterEnemyTracker = GetComponent<EncounterEnemyTracker>();
    }

    public void DisplayEnemy(EnemyData enemyData)
    {
        if (enemyPositions != null)
        {
            // Instantiate the enemy prefab
            GameObject newEnemy = Instantiate(enemyPrefab, enemyPositions[0].position, Quaternion.identity, enemyPositions[0]);

            Enemy enemy = newEnemy.GetComponent<Enemy>();

            // Set the sprite
            enemy.spriteRenderer.sprite = enemyData.enemySprite;

            // Adjust collider based on sprite size

            Vector2 S = enemy.spriteRenderer.sprite.bounds.size;
            enemy.boxCollider.size = S;

            // Assign enemy data
            enemy.enemyData = enemyData;
            enemy.UpdateEnemyDisplay();

            // Add to list
            enemyReferences.Add(enemy);

            if (encounterEnemyTracker != null)
            {
                if (enemy != null)
                {
                    encounterEnemyTracker.AddEnemy(enemy);
                }
            }
            else Debug.Log("encounterEnemyTracker null in EnemyDisplayManager");
        }
        else Debug.Log("EnemyPositions were null in EnemyDisplayManager");
    }

}
