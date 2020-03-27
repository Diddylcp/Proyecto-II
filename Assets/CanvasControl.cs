using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
       transform.LookAt(Camera.main.transform.position, Camera.main.transform.up);
    }
}
