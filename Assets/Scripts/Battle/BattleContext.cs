using UnityEngine;

public class BattleContext : MonoBehaviour
{
    public static BattleContext Instance { get; private set; }

    public BattleManager battleManager;
    public CardDatabase cardDatabase;
    public CardPlayManager cardPlayManager;
    public DefaultDeckCreator defaultDeckCreator;
    public DiscardManager discardManager;
    public DrawPileManager drawPileManager;
    public HandManager handManager;
    public Hero playerHero;
    public CardRewardManager rewardManager;


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
