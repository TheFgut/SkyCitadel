using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAtackTarget
{
    public bool isAlive {  get; }

    public Vector3 position { get; }
}
