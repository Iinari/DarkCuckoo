using UnityEngine.UI;
using TMPro;
using UnityEngine;
using SnIProductions;
using UnityEditor.ShaderGraph.Internal;
using Unity.Properties;

public class MoonVisual : MonoBehaviour
{
    public Image moonImg;

    public TMP_Text cycleTxt;


    public void FillMoon(float fillAmount, float moonState)
    {
        moonImg.fillAmount = fillAmount;
        cycleTxt.text = moonState.ToString();
    }

}
