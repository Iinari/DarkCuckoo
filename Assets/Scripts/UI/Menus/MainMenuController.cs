using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string gameplaySceneName = "BattleScene";

    public void OnNewGamePressed()
    {
        DataPersistenceManager.Instance.NewGame();

        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OnLoadGamePressed()
    {
        DataPersistenceManager.Instance.LoadGame();

        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}