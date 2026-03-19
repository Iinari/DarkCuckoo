using UnityEngine;
using System.Collections;

//Directs the game flow, when starting a new game or when game was loaded from a save
public class BattleBootstrap : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;

    void Start()
    {
        if (DataPersistenceManager.Instance.HasLoadedGame)
        {
            StartCoroutine(LoadBattle());
        }
        else
        {
            StartCoroutine(NewBattle());
        }
    }

    IEnumerator NewBattle()
    {
        yield return null;

        DataPersistenceManager.Instance.NewGame();
        battleManager.StartBattle();
    }

    IEnumerator LoadBattle()
    {
        Debug.Log("LoadBattle");
        yield return null;

        DataPersistenceManager.Instance.LoadGame();
        battleManager.StartLoadedBattle();
    }
}