using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float moveSpeed;
    public float zoomSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float XAxis = Input.GetAxis("Horizontal") * moveSpeed;
        float YAxis = Input.GetAxis("Vertical") * moveSpeed;
        float zoomAxis = 0;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            zoomAxis = 1;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            zoomAxis = -1;
        }
        transform.position += new Vector3(XAxis, YAxis, zoomAxis * zoomSpeed) * Time.deltaTime;
    }
}
