using UnityEngine.UI;
using TMPro;
using UnityEngine;
using SnIProductions;
using UnityEditor.ShaderGraph.Internal;

public class MoonCycle : MonoBehaviour
{
    public Image moonImg;

    public TMP_Text cycleTxt;

    private float moonCycleLength = 10;

    private float moonCycleMin = 0;

    private float moonCycleCurrent = 10;

    public void UpdateMoon()
    {
        moonCycleCurrent++;
        if (moonCycleCurrent > moonCycleLength)
        {
            moonCycleCurrent = moonCycleMin;
        }
        if (moonCycleCurrent != moonCycleMin)
        {
            float moonProsent = moonCycleCurrent / moonCycleLength;
            moonImg.fillAmount = moonProsent;
        }
        else
        {
            moonImg.fillAmount = moonCycleMin;
        }

        cycleTxt.text = moonCycleCurrent.ToString();

    }
  
}
