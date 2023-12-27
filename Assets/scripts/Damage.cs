using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Damage
{
    [SerializeField] private float _value;
    public float value { get { return _value; } }

    [SerializeField] private DamageType damageType;
    public DamageType type { get { return damageType; } }
}


public enum DamageType
{
    physical,
    fire,
    acid,
    piercing
}
