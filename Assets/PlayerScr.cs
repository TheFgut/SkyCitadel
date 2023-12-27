using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScr : MonoBehaviour
{
    Camera cam;
    public Vector3 mouseP;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
         
        Vector3 R = cam.ScreenPointToRay(Input.mousePosition).direction;
        float coef = cam.transform.position.z / R.z;
        mouseP = new Vector3(-R.x * coef, -R.y * coef, 0);
    }

    public Vector3 GetMPos()
    {
        //получает позицию курсора в пространстве
        return mouseP + new Vector3(transform.position.x, transform.position.y);
    }
}
