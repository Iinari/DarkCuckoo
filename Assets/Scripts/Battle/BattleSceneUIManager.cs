using System;
using UnityEngine;
using UnityEngine.Rendering;

public class BattleSceneUIManager : MonoBehaviour
{
    [SerializeField] GameObject drawPileDisplayPrefab;
    [SerializeField] GameObject resultPrefab;

    private ResultPopUp resultPopUp;
    private DrawPileDisplay drawPileDisplay;

    public Transform BattleTransform;

    public void SceneUISetUp()
    {
        GameObject results = Instantiate(resultPrefab, BattleTransform.position, Quaternion.identity, BattleTransform);
        resultPopUp = results.GetComponent<ResultPopUp>();
        results.gameObject.SetActive(false);
        
        GameObject drawPile = Instantiate(drawPileDisplayPrefab, BattleTransform.position, Quaternion.identity, BattleTransform);
        drawPileDisplay = drawPile.GetComponent<DrawPileDisplay>();
        drawPile.gameObject.SetActive(false);

    }

    public void OpenResultScreen(bool playerDied)
    {
        if (playerDied) 
        {
            if (resultPopUp != null)
            {
                resultPopUp.OpenDeathScreen();
            }
            else Debug.Log("ResultPopUp Null");
        }
        else
        {
            if (resultPopUp != null)
            {
                resultPopUp.OpenVictoryScreen();
            }
            else Debug.Log("ResultPopUp Null");
        }
    }

    public void ActivateDrawPileDisplay()
    {
        TryGetDrawPileDisplayRef();
        if (drawPileDisplay != null)
        {
            drawPileDisplay.OpenDrawPileDisplay();
        }
    }

    public void TryGetDrawPileDisplayRef()
    {
        if (drawPileDisplay == null)
        {
            drawPileDisplay = FindAnyObjectByType<DrawPileDisplay>(FindObjectsInactive.Include);
            if (drawPileDisplay == null)
            {
                Debug.Log("DrawPileDisplay ref is null");
            }
        }
    }
}
