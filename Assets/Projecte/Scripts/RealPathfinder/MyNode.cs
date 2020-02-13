using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNode : MonoBehaviour
{
    public Vector2 pos;
    public List<MyNode> neighbours;
    public float gCost;
    public float hCost;
    public MyNode parent;

    private void Awake()
    {
        pos = transform.position;
    }

    public float fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int CompareTo(MyNode nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
