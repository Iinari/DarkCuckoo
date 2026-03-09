using UnityEngine;

public class PlayerCardsDataHandler : MonoBehaviour, IDataPersistence
{
    private DeckManager deckManager;
    private HandManager handManager;
    private DiscardManager discardManager;
    private DrawPileManager drawPileManager;


    void Awake()
    {
        deckManager = GetComponentInChildren<DeckManager>();
        handManager = GetComponentInChildren<HandManager>();
        discardManager = GetComponent<DiscardManager>();
        drawPileManager = GetComponent<DrawPileManager>();
    }
    private void OnEnable()
    {
        DataPersistenceManager.Instance?.RegisterDataPersistenceObject(this);
    }

    private void OnDisable()
    {
        DataPersistenceManager.Instance?.UnregisterDataPersistenceObject(this);
    }

    public void LoadData(GameData data)
    {

    }

    public void SaveData(ref GameData data)
    {

    }

    public void ResetToDefault(ref GameData data)
    {

    }
}
