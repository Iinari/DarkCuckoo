using UnityEngine;
using System.Collections;

//Directs the game flow, when starting a new game or when game was loaded from a save
public class BattleBootstrap : MonoBehaviour
{
    BattleInitiator battleInitiator;

    void Start()
    {
        battleInitiator = FindFirstObjectByType<BattleInitiator>();

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

        DataPersistenceManager.Instance.ResetDataToDefault();
        battleInitiator.SetupNewBattle();
    }

    IEnumerator LoadBattle()
    {
        yield return null;

        DataPersistenceManager.Instance.LoadGame();
        battleInitiator.ResumeBattle();
    }
}