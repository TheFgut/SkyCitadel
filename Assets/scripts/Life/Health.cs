using UnityEngine;

[System.Serializable]
public class Health
{
    [SerializeField]private float maxValue;

    public float value {  get; private set; }

    public voidCallback onHpFinishes;

    public void ApplyPureDamage(float pureDamage)
    {
        value -= pureDamage;
        if (value < 0)
        {
            onHpFinishes?.Invoke();
            value = 0;
            return;
        }
    }

    public void PureHeal(float pureHealing)
    {
        value += pureHealing;
        if(value > maxValue)
        {
            value = pureHealing;
        }
    }
}

public delegate void voidCallback();
