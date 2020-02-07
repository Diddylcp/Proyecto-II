using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequestManager : MonoBehaviour
{
    Queue<PathRequest> pathRequestsQueue = new Queue<PathRequest>();
    PathRequest currPathRequest;

    static PathRequestManager instance;
    Pathfinding pathfinding;

    bool isProcessingPath;

    private void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }

    public static void RequestPath(Vector2 start, Vector2 end, Action<Vector2[], bool> callback)
    {
        PathRequest newPathRequest = new PathRequest(start, end, callback);
        instance.pathRequestsQueue.Enqueue(newPathRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if(!isProcessingPath && pathRequestsQueue.Count > 0)
        {
            currPathRequest = pathRequestsQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currPathRequest.pathStart, currPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector2[] path, bool success)
    {
        currPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector2 pathStart;
        public Vector2 pathEnd;
        public Action<Vector2[], bool> callback;

        public PathRequest(Vector2 _start, Vector2 _end, Action<Vector2[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }
    }
}
