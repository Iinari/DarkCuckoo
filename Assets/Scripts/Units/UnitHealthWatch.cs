using Unity.VisualScripting;
using SnIProductions;
using UnityEngine;

public class UnitHealthWatch : MonoBehaviour
{

    //Ajatus olisi, että onko luokka joka kuuntelee kunkin Unitin healthia tai tarkemmin tarkkailee vain onko unit hengissä
    private AttributesManager attributesManager;

    private UnitHealthState State;

    private void Awake()
    {
        attributesManager = GetComponent<AttributesManager>();
        State = GetComponent<UnitHealthState>();
    }

    private void Update()
    {
        if (attributesManager != null)
        {
            if (attributesManager.hp == attributesManager.fullHealth)
            {
                return;
            }
            if (attributesManager.hp > 0)
            {
                State.SetState(HealthState.Alive);
                return;
            }
            else
            {
                State.SetState(HealthState.Dead);
            }
        }
    }

    public void TakeDamage(float incomingDmg)
    {
        attributesManager.ModifyAttribute(AttributesManager.Attribute.HP, incomingDmg);
    }
}
