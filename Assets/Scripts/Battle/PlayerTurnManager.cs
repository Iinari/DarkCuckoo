using SnIProductions;
using UnityEngine;

public class PlayerTurnManager : MonoBehaviour
{
    private BattleStateStatus state;
    private Hero player;

    private DeckManager deckManager;
    private DrawPileManager drawPileManager;
    private HandManager handManager;
    private DiscardManager discardManager;

    private void Awake()
    {
        state = GetComponent<BattleStateStatus>();
        
        player = FindFirstObjectByType<Hero>();

        deckManager = GetComponentInChildren<DeckManager>();
        drawPileManager = GetComponentInChildren<DrawPileManager>();
        handManager = GetComponentInChildren<HandManager>();
        discardManager = GetComponentInChildren<DiscardManager>();
        
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
        drawPileManager.StartPlayerTurn();
        if (player == null) 
        {
            TryGetPlayerRef();
        }
        if (player != null)
        {
            player.GetComponent<AttributesManager>().RestoreAttribute(AttributesManager.Attribute.MP);
        }
        
    }

    public void EndPlayerTurn()
    {
        for (int i = 0; i < handManager.handCardObjs.Count; i++)
        {
            discardManager.AddToDiscard(handManager.handCardObjs[i].GetComponent<Card>().cardData);
            Destroy(handManager.handCardObjs[i]);
        }
        handManager.handCardObjs.Clear();
        handManager.UpdateHandVisuals();
    }

    public void TryGetPlayerRef()
    {
        player = FindFirstObjectByType<Hero>();
    }
}
