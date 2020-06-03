using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHerself : MonoBehaviour
{
    private float rotation = 0;
    void Update()
    {
        rotation -= Time.deltaTime * 30;
        this.transform.rotation = Quaternion.Euler(0, rotation, rotation);
    }
}
