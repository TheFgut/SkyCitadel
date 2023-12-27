using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeModule : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private DamageReciever[] recievers;


    private void Start()
    {
        health.onHpFinishes += moduleDestroyed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] points = collision.contacts;
        Collider2D col = points[0].otherCollider;
        //for contact damage
        foreach (var reciever in recievers)
        {
            
            if (reciever.hasSameCollider(col))
            {
                IHasContactDamage contactDamageModule = collision.gameObject.GetComponent<IHasContactDamage>();
                if(contactDamageModule != null)
                {
                    //ApplyDamage(contactDamageModule.getContactDamage());
                }
                break;
            }
        }
    }

    public voidCallback onDestroyModule;

    private void moduleDestroyed()
    {
        onDestroyModule?.Invoke();
    }

    public void ApplyDamage(Collider2D applyToCollider, Damage[] damage)
    {
        foreach (var reciever in recievers)
        {
            if (reciever.hasSameCollider(applyToCollider))
            {
                float pureDamage = reciever.getPureDamage(damage);
                health.ApplyPureDamage(pureDamage);
                break;
            }
        }
    }


}
