using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTest : MonoBehaviour
{
    public Transform target;
    float speed = 10;
    Vector2[] path;
    int targetIndex;

    private void Start()
    {
        PathRequestManager.RequestPath((Vector2)transform.position, (Vector2)target.position, OnPathFound);
    }

    private void Update()
    {
             
    }
    public void OnPathFound(Vector2[] newPath, bool success)
    {
        if (success)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector2 currWaypoint = path[0];
        targetIndex = 0;

        while (true)
        {
            if((Vector2)transform.position == currWaypoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    PathRequestManager.RequestPath((Vector2)transform.position, (Vector2)target.position, OnPathFound);
                    yield break;
                }
                currWaypoint = path[targetIndex];
            }
            transform.position = Vector2.MoveTowards((Vector2)transform.position, currWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if(path != null)
        {
            for(int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector2.one);

                if(i == targetIndex)
                {
                    Gizmos.DrawLine((Vector2)transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
