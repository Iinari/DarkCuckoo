using UnityEngine;

[System.Serializable]
public class Stat
{
    public float current;
    public float max;

    public Stat(float current, float max)
    {
        this.current = current;
        this.max = max;
    }

    public void ModifyCurrent(float value)
    {
        current = Mathf.Clamp(current + value, 0, max);
    }

    public void ModifyMax(float value, bool fillIncrease = false)
    {
        max = Mathf.Max(0, max + value);

        if (fillIncrease && value > 0)
            current += value;

        current = Mathf.Clamp(current, 0, max);
    }

    public void SetMax(float value)
    {
        max = Mathf.Max(0, value);
        current = Mathf.Clamp(current, 0, max);
    }

    public void Restore()
    {
        current = max;
    }   
}