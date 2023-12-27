using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageReciever
{
    [SerializeReference] private Collider2D recieverCollider;
    [SerializeField] private Resist[] resists;
    public bool hasSameCollider(Collider2D col)
    {
        return ReferenceEquals(recieverCollider, col);
    }


    public float getPureDamage(Damage[] damage)
    {
        float pure = 0;
        foreach(Damage dmg in damage)
        {
            int typeID = (int)dmg.type;
            if(typeID >= resists.Length)
            {
                pure += dmg.value;
            }
            else
            {
                pure += dmg.value * resists[typeID].value;
            }
        }
        return pure;
    }

    [System.Serializable]
    private class Resist
    {
        [SerializeField] private DamageType type;
        [SerializeField] private float damageMultiplier = 1;
        public float value { get { return damageMultiplier; } }
    }

}
