using UnityEngine;

public class BattleContext : MonoBehaviour
{
    public static BattleContext Instance { get; private set; }

    public HandManager handManager;
    public DrawPileManager drawPileManager;
    public DiscardManager discardManager;
    public CardPlayManager cardPlayManager;
    public BattleManager battleManager;
    public Hero playerHero;
    public DefaultDeckCreator defaultDeckCreator;

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
