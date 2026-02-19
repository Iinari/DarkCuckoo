using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public AudioManager AudioManager { get; private set; }

    public OptionsManager OptionsManager { get; private set; }


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"Setting GameManager Instance to {this}");
            InitializeManagers();
        }
        //Make sure there can only be 1 GameManager at any time
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    //Initialize other managers, such as Audio and Options. If the ChildComponent is not found create one dynamically
    private void InitializeManagers()
    {
        AudioManager = GetComponentInChildren<AudioManager>();
        OptionsManager = GetComponentInChildren<OptionsManager>();
    
        if (AudioManager == null )
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/AudioManager");
            if( prefab == null)
            {
                Debug.Log("AudioMananger Prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                AudioManager = GetComponentInChildren<AudioManager>();
            }
        }

        if (OptionsManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/OptionsManager");
            if (prefab == null)
            {
                Debug.Log("OptionsManager Prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                OptionsManager = GetComponentInChildren<OptionsManager>();
            }
        }

    }

}
