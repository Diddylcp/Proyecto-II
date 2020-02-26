using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GraphPathfinder
{
    public Vector2[] waypoints;

    public GraphPathfinder()
    {
       
    }

    public bool findPath(MyNode startNode, MyNode endNode)
    {
        if(startNode == endNode)
        {
            waypoints[0] = startNode.pos;
            return true;
        }
        List<MyNode> openList = new List<MyNode>();
        HashSet<MyNode> closedList = new HashSet<MyNode>();
        MyNode currNode = startNode;
        currNode.hCost = Vector2.Distance(currNode.pos, endNode.pos);
        currNode.gCost = 0;
        while(currNode != endNode)
        {
            foreach(MyNode neighbour in currNode.neighbours)    //Miro que vecinos tiene el currNode
            {
                if (!closedList.Contains(neighbour) && !openList.Contains(neighbour))
                {
                    neighbour.gCost = currNode.gCost + Vector2.Distance(neighbour.pos, currNode.pos);
                    neighbour.hCost = Vector2.Distance(neighbour.pos, endNode.pos);
                    //if(neighbour.parent == null || neighbour.CompareTo(currNode) < 0)
                    if(!closedList.Contains(neighbour) || neighbour.CompareTo(currNode) < 0)
                    {
                        neighbour.parent = currNode;    //Si la f del vecino es menor currNode pasa a ser el parent del vecino
                    }
                    openList.Add(neighbour);
                }
            }
            closedList.Add(currNode);
            MyNode min = openList[0];
            foreach(MyNode node in openList)    //Busco el nodo con el valor f más pequeño
            {
                if(node.CompareTo(min) < 0)
                {
                    min = node;
                }
            }
            currNode = min;
            openList.Remove(min);
            openList.TrimExcess();
        }

        RetracePath(startNode, endNode);
        
        return true;
    }

    void RetracePath(MyNode startNode, MyNode endNode)
    {
        List<Vector2> path = new List<Vector2>();
        MyNode currNode = endNode;

        while (currNode.gCost != 0)
        {
            if(!path.Contains(currNode.pos))
                path.Add(currNode.pos);
            currNode = currNode.parent;
        }

        waypoints = path.ToArray();
        Array.Reverse(waypoints);
        
    }
}
