using UnityEngine;

public class UnitHealthWatch : MonoBehaviour
{

    //Ajatus olisi, että onko luokka joka kuuntelee kunkin Unitin healthia tai tarkemmin tarkkailee vain onko unit hengissä
    private AttributesManager attributesManager;

    private void Awake()
    {
        attributesManager = GetComponent<AttributesManager>();
    }

    private void Update()
    {
        if (attributesManager != null)
        {

        }
    }
}
