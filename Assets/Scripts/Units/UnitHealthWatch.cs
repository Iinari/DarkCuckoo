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
        attributesManager = GameSession.Instance.AttributesManager;
        State = GetComponent<UnitHealthState>();
    }

    private void Update()
    {
        if (attributesManager != null)
        {
            if (attributesManager.GetStat(StatType.Health) == attributesManager.GetMaxStat(StatType.Health))
            {
                return;
            }
            if (attributesManager.GetStat(StatType.Health) > 0)
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
        attributesManager.ModifyStat(StatType.Health, incomingDmg);
        //attributesManager.ModifyAttribute(AttributesManager.Attribute.HP, incomingDmg);
    }
}
