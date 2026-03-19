using SnIProductions;
using System;
using UnityEngine;
using UnityEngine.Rendering;

public class BattleScenePopUpManager : MonoBehaviour
{
    //Gameobjects of pop ups
    [SerializeField] GameObject cardDisplayPrefab; //Both draw and discard display use the same prefab
    [SerializeField] GameObject resultPrefab;

    //Scripts of pop ups
    private ResultPopUp resultPopUp;
    private CardPopUpDisplay cardPileDisplay;

    public Transform BattleTransform;

    private UnitHealthState state;

    private void OnEnable()
    {
        BattleEvents.OnBattleStarted += NewBattle;
        BattleEvents.OnBattleLoaded += ResumeBattle;
        BattleEvents.OnBattleEnded += OpenResultScreen;
    }

    private void OnDisable()
    {
        BattleEvents.OnBattleStarted -= NewBattle;
        BattleEvents.OnBattleLoaded -= ResumeBattle;
        BattleEvents.OnBattleEnded -= OpenResultScreen;
    }


    public void NewBattle()
    {
        GameObject results = Instantiate(resultPrefab, BattleTransform.position, Quaternion.identity, BattleTransform);
        resultPopUp = results.GetComponent<ResultPopUp>();
        results.SetActive(false);

        GameObject cardDisplay = Instantiate(cardDisplayPrefab, BattleTransform.position, Quaternion.identity, BattleTransform);
        cardPileDisplay = cardDisplay.GetComponent<CardPopUpDisplay>();
        cardDisplay.SetActive(false);

        state = BattleContext.Instance.playerHero.GetComponent<UnitHealthState>();
        state.OnStateChanged += OnStateChanged;
    }
    public void ResumeBattle()
    {
        GameObject results = Instantiate(resultPrefab, BattleTransform.position, Quaternion.identity, BattleTransform);
        resultPopUp = results.GetComponent<ResultPopUp>();
        results.SetActive(false);

        GameObject cardDisplay = Instantiate(cardDisplayPrefab, BattleTransform.position, Quaternion.identity, BattleTransform);
        cardPileDisplay = cardDisplay.GetComponent<CardPopUpDisplay>();
        cardDisplay.SetActive(false);

        state = BattleContext.Instance.playerHero.GetComponent<UnitHealthState>();
        state.OnStateChanged += OnStateChanged;
    }
    private void OnStateChanged(HealthState newState)
    {
        if (newState == HealthState.Dead) 
        {
            if (resultPopUp != null)
            {
                resultPopUp.OpenDeathScreen();
            }
            else Debug.Log("ResultPopUp Null");
        }
    }

    public void OpenResultScreen(BattleResult result)
    {
        switch (result)
        {
            case BattleResult.Won:
                if (resultPopUp != null)
                {
                    resultPopUp.OpenVictoryScreen();
                }
                else Debug.Log("ResultPopUp Null");
                break;
            case BattleResult.Lost:
                if (resultPopUp != null)
                {
                    resultPopUp.OpenDeathScreen();
                }
                else Debug.Log("ResultPopUp Null");
                break;
        }
    }

    //Called by player clicking drawpile
    public void ActivateDrawPileDisplay()
    {
        TryGetDrawPileDisplayRef();
        if (cardPileDisplay != null)
        {
            cardPileDisplay.OpenCardDisplay(false);
        }
    }

    //Called by player clicking discard pile
    public void ActivateDiscardDisplay()
    {
        TryGetDrawPileDisplayRef();
        if (cardPileDisplay != null)
        {
            cardPileDisplay.OpenCardDisplay(true);
        }
    }

    public void TryGetDrawPileDisplayRef()
    {
        if (cardPileDisplay == null)
        {
            cardPileDisplay = FindAnyObjectByType<CardPopUpDisplay>(FindObjectsInactive.Include);
            if (cardPileDisplay == null)
            {
                Debug.Log("DrawPileDisplay ref is null");
            }
        }
    }
}
