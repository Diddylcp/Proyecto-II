using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    // Position in the Node Array
    public int gridX;
    public int gridY;

    public bool IsWall;     // Tells if we can walk in the node
    public Vector3 Position;   // The position of the node in game

    public Node Parent;     // Previous Node

    public int gCost;       // Cost moving to the next square
    public int hCost;       // Distance to the goal from this Node
    public int FCost { get { return gCost + hCost; } }  // Final distance
       
    public Node(bool a_IsWall, Vector3 a_Pos, int a_gridX, int a_gridY) // Constructor
    {
        IsWall = a_IsWall;
        Position = a_Pos;
        gridX = a_gridX;
        gridY = a_gridY;
    }
}
