using SnIProductions;
using UnityEngine;

public class PlayerTurnManager : MonoBehaviour
{
    private BattleStateStatus state;
    private DeckManager deckManager;
    private Hero player;

    private void Awake()
    {
        state = GetComponent<BattleStateStatus>();
        deckManager = GetComponent<DeckManager>();
        player = FindFirstObjectByType<Hero>();
        
        state.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(BattleState newState)
    {
        if (newState == BattleState.PlayerTurn)
        {
            StartPlayerTurn();
        }
        if (newState == BattleState.EnemyTurn)
        {
            EndPlayerTurn();
        }
    }

    public void StartPlayerTurn()
    {
        deckManager.StartPlayersTurn();
        if (player == null) 
        {
            TryGetPlayerRef();
        }
        if (player != null)
        {
            player.attributesManager.RestoreAttribute(AttributesManager.Attribute.MP);
        }
        
    }

    public void EndPlayerTurn()
    {
        deckManager.EndPlayersTurn();
    }

    public void TryGetPlayerRef()
    {
        player = FindFirstObjectByType<Hero>();
    }
}
