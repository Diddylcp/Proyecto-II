using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetBlock : MonoBehaviour
{
    public LayerMask hitLayers;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))   // if the player has left clicked
        {
            Vector3 mouse = Input.mousePosition;    // Get the mouse position
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);    // Cast a ray to get where the mouse is pointing at
            RaycastHit hit; //Store where the Ray hits
            if(Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))  // If the raycast doesn't hit a wall
            {
                this.transform.position = hit.point;    // Move the target to the mouse position
            }
        }
    }
}
