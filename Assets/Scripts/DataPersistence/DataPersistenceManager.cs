using UnityEditor.Toolbars;
using UnityEngine;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;


public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects = new();

    private FileDataHandler dataHandler;

    public static DataPersistenceManager Instance {  get; private set; }

    private void Awake()
    {
        if (Instance != null) 
        {
            Debug.LogError("Found more than one DataPersistenceManager in the scene");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //This is done for the editor testing purposes real game always already has DataPersistenceManager existing
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void AutoCreate()
    {
        if (Instance == null)
        {
            GameObject obj = new GameObject("DataPersistenceManager");
            obj.AddComponent<DataPersistenceManager>();
            obj.GetComponent<DataPersistenceManager>().fileName = "data.json";
            obj.GetComponent<DataPersistenceManager>().useEncryption = true;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        LoadGame();
    }

    public void RegisterDataPersistenceObject(IDataPersistence obj)
    {
        if (!dataPersistenceObjects.Contains(obj))
        {
            dataPersistenceObjects.Add(obj);
        }         
    }

    public void UnregisterDataPersistenceObject(IDataPersistence obj)
    {
        dataPersistenceObjects.Remove(obj);
    }


    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }
        else
        {
            foreach (var obj in dataPersistenceObjects)
            {
                obj.LoadData(gameData);
            }
        }    
    }

    public void SaveGame() 
    {
        foreach (var obj in dataPersistenceObjects)
        {
            obj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    /*private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
           .OfType<IDataPersistence>()
           .ToList();
    }*/
}
