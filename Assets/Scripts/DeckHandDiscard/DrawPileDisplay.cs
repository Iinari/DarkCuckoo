using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DrawPileDisplay : PopUp
{
    public void OpenDrawPileDisplay()
    {
        gameObject.SetActive(true);   
    }

    public void CloseDrawPileDisplay() 
    {
        gameObject.SetActive(false);
    }
}
