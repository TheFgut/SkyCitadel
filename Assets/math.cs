using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class math
{
    public static Vector3 rotVector(Vector3 vect, Quaternion rotation)
    {
        float magn = vect.magnitude;
        vect.Normalize();
        Quaternion vectQ = new Quaternion(vect.x, vect.y, vect.z, 0);
        vectQ = rotation * vectQ * Quaternion.Inverse(rotation);
        return new Vector3(vectQ.x, vectQ.y, vectQ.z) * magn;
    }
}
