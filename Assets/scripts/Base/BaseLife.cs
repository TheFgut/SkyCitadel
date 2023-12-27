using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLife : MonoBehaviour
{

    [SerializeField] private int maxLifePoints;
    public int life { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        life = maxLifePoints;
    }
    
    public bool destroyed { get; private set; }
    public void DecreaseLifePoints(int amount)
    {
        life -= amount;

        if(life <= 0 && destroyed == false)
        {
            destroyed = true;
        }
    }
}
