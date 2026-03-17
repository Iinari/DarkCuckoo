using UnityEngine;

public class BattleContext : MonoBehaviour
{
    public static BattleContext Instance { get; private set; }

    public HandManager handManager;
    public DrawPileManager drawPileManager;
    public DiscardManager discardManager;
    public BattleInitiator battleInitiator;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
