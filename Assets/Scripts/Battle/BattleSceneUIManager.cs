using UnityEngine;
using UnityEngine.Rendering;

public class BattleSceneUIManager : MonoBehaviour
{
    [SerializeField] GameObject drawPilePrefab;
    [SerializeField] GameObject resultPrefab;

    private ResultPopUp resultPopUp;
    private DrawPileDisplay drawPileDisplay;

    public Transform BattleTransform;

    public void Awake()
    {
        GameObject results = Instantiate(resultPrefab, BattleTransform.position, Quaternion.identity, BattleTransform);
        resultPopUp = results.GetComponentInChildren<ResultPopUp>();

        
        results.gameObject.SetActive(false);
        

        GameObject drawPile = Instantiate(drawPilePrefab, BattleTransform.position, Quaternion.identity, BattleTransform);
        drawPileDisplay = drawPile.GetComponentInChildren<DrawPileDisplay>();
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
        else Debug.Log("DrawPileDisplay Null");

    }

    public void TryGetDrawPileDisplayRef()
    {
        drawPileDisplay = FindFirstObjectByType<DrawPileDisplay>();
    }
}
