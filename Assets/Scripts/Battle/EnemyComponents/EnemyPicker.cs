using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using SnIProductions;
using UnityEngine.AI;

public class EnemyPicker : BattleComponent
{
    //These are values that tester might want to change in editor, that is why these are on top
    public EnemyData ChosenEnemy;

    public List<EnemyData> allEnemies = new();

    public override void BattleSetUp(BattleSystem battleSystem)
    {
        //Enemy can be chosen in Scene, if enemy is not set set randon enemy from the list
        if (ChosenEnemy == null)
        {
            //This is just a testing phase code, later this needs logic for choosing the enemy for right type/level etc.
            EnemyData[] enemies = Resources.LoadAll<EnemyData>("Enemies");
            allEnemies.AddRange(enemies);

            if (allEnemies.Count > 0)
            {
                Utility.Shuffle(allEnemies);
                GetComponent<EnemyDisplayManager>().DisplayEnemy(allEnemies[0]);
            }
        }
        else
        {
            GetComponent<EnemyDisplayManager>().DisplayEnemy(ChosenEnemy);
        }
    }
}
