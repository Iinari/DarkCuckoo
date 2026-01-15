using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPopUp : PopUp
{

    [SerializeField] TMP_Text resultTxt;
    [SerializeField] TMP_Text pointsTxt;


    [SerializeField] Transform deathScreenParent;
    [SerializeField] Transform victoryScreenParent;

    [SerializeField] GameObject rewardBtn1;
    [SerializeField] GameObject rewardBtn2;
    [SerializeField] Button rewardBtn3;

    public Transform[] deathScreenComponents;
    public Transform[] victoryScreenComponents;

    private string deathScreenTxt = "You died";
    private string victoryScreenTxt = "Victory";


    public void OpenVictoryScreen()
    {
        gameObject.SetActive(true);
        SetUpLists();

        resultTxt.text = victoryScreenTxt;

        foreach (Transform child in deathScreenComponents)
        {
            child.gameObject.SetActive(false);
        }

        GetComponentInChildren<RewardManager>().DisplayRewardCards();
    }

    public void OpenDeathScreen()
    {
        gameObject.SetActive(true);
        SetUpLists();
        resultTxt.text = deathScreenTxt;

        foreach (Transform child in victoryScreenComponents)
        {
            child.gameObject.SetActive(false);
        }

    }

    public void SetUpLists()
    {
        //NEED TO tehdä listojen tyhjennys tähän

        deathScreenComponents = new Transform[deathScreenParent.transform.childCount];

        for (int i = 0; i < deathScreenComponents.Length; i++)
        {
            deathScreenComponents[i] = deathScreenParent.transform.GetChild(i);
        }

        victoryScreenComponents = new Transform[victoryScreenParent.transform.childCount];

        for (int i = 0; i < victoryScreenComponents.Length; i++)
        {
            victoryScreenComponents[i] = victoryScreenParent.transform.GetChild(i);
        }
    }
}
