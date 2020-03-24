using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 newPos = hit.point;
            transform.position = newPos;
        }

    }
}
