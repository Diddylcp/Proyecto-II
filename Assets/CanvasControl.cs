using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour
{
    float rotX;

    void Start()
    {
        
        rotX = Camera.main.transform.eulerAngles.x * (-1);
        transform.Rotate(rotX, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(Camera.main.transform.position, Camera.main.transform.up);
       // transform.Rotate(rotX, 0, 0);
    }
}
