using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinding : MonoBehaviour
{
    PathRequestManager requestManager;
    Grid grid;

    private void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
    }

    public void StartFindPath(Vector2 startPos, Vector2 endPos)
    {
        StartCoroutine(FindPath(startPos, endPos));
    } 

    IEnumerator FindPath(Vector2 startPos, Vector2 targetPos)
    {
        Vector2[] waypoints = new Vector2[0];
        bool success = false;

        Node startNode = grid.GetNodeFromWorldPoint(startPos);
        Node targetNode = grid.GetNodeFromWorldPoint(targetPos);

        if (startNode.walkable && targetNode.walkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currNode = openSet.RemoveFirst();
                closedSet.Add(currNode);

                if (currNode == targetNode)
                {
                    success = true;
                    break;
                }

                foreach (Node neighbour in grid.GetNeighbours(currNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour)) continue;

                    int moveCost = currNode.gCost + GetDistanceNodes(currNode, neighbour);
                    if (moveCost < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = moveCost;
                        neighbour.hCost = GetDistanceNodes(neighbour, targetNode);
                        neighbour.parent = currNode;

                        if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                        else openSet.UpdateItem(neighbour);
                    }
                }
            }
        }
        yield return null;
        if(success) waypoints = RetracePath(startNode, targetNode);

        requestManager.FinishedProcessingPath(waypoints, success);
    }

    Vector2[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currNode = endNode;

        while(currNode != startNode)
        {
            path.Add(currNode);
            currNode = currNode.parent;
        }
        Vector2[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    
    }

    Vector2[] SimplifyPath(List<Node> path)
    {
        List<Vector2> waypoints = new List<Vector2>();
        Vector2 lastDir = Vector2.zero;

        for(int i = 1; i < path.Count; i++)
        {
            Vector2 newDir = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if(newDir != lastDir)
            {
                waypoints.Add(path[i].worldPos);
            }
            lastDir = newDir;
        }

        return waypoints.ToArray();
    }

    int GetDistanceNodes(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY) return 14 * distY + 10 * (distX - distY);

        return 14 * distX + 10 * (distY - distX);
    }

}
