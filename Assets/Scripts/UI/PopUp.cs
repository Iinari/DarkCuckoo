using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PopUp : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject popUpPrefab;
    [SerializeField] Button continueButton;
  
    public virtual void ActivatePopUp() { }

    public virtual void DeactivatePopUp() { }
}
