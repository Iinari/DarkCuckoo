using SnIProductions;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class EnemyTurnManager : MonoBehaviour
{
    private BattleStateStatus state;

    private EncounterEnemyTracker enemyTracker;

    private void Awake()
    {
        state = GetComponent<BattleStateStatus>();
        enemyTracker = GetComponent<EncounterEnemyTracker>();
        state.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(BattleState newState)
    {
        if(newState == BattleState.EnemyTurn)
        {
            TakeEnemyTurn();
        }
    }

    public void TakeEnemyTurn()
    {
        for (int i = 0; i < enemyTracker.Enemies.Count; i++) 
        {
            enemyTracker.Enemies[i].TakeTurn();
        }
        state.SetState(BattleState.PlayerTurn);
    }
}
