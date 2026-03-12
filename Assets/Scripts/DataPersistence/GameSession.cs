using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    public AttributesManager AttributesManager { get; private set; }
    public DeckManager DeckManager { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        AttributesManager = GetComponent<AttributesManager>();
        DeckManager = GetComponent<DeckManager>();
    }
}