using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DrawPileDisplay : MonoBehaviour
{
    [SerializeField] GameObject drawPileDisplayPrefab;
    [SerializeField] Button continueButton;


    public void OpenDrawPileDisplay()
    {
        Debug.Log("Tried to open display");
        gameObject.SetActive(true);

        Debug.Log("Popup Active? " + gameObject.activeSelf);      
    }

    public void CloseDrawPileDisplay() 
    {
        gameObject.SetActive(false);
    }
}
