using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EncounterEnemyTracker : MonoBehaviour
{
    public List<Enemy> Enemies { get; private set; } = new List<Enemy>();

    public Enemy enemy { get; private set; }

    public void SetEnemy(Enemy newEnemy)
    {
        enemy = newEnemy;
    }

    public void SetEnemies(List<Enemy> newEnemies)
    {
        Enemies = newEnemies;
    }

    public void AddEnemy(Enemy newEnemy)
    {
        if (newEnemy != null)
        {
            Enemies.Add(newEnemy);
        }
        else Debug.Log("empty enemy");
    }
}
