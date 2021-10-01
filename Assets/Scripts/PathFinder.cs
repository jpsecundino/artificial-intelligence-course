using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Threading;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public GameObject target;
    public float travelTime;
    
    private WorldGrid _worldGrid;
    private Queue<Vector2> path;
    
    private float timeElapsed;
    private float lerpDuration;

    private Vector2 startPos;
    private Vector2 endPos;
    private float valueToLerp;

    private void Start()
    {
        _worldGrid = WorldGrid.Instance;
        
        bool[,] visited = NewVisitedArray(_worldGrid.Width, _worldGrid.Height);
        
        path = new Queue<Vector2>();

        Vector2 curPos = _worldGrid.WorldPos2GridIdx(transform.position); 
        
        dfs(curPos, ref visited);

        Debug.Log(path.Count);

        GetComponent<DiscretePositioning>().enabled = false;
        
        lerpDuration = travelTime / path.Count;
        startPos = path.Dequeue();
        endPos = path.Dequeue();

    }

    void Update()
    {
        
        if (timeElapsed < lerpDuration)
        {
            Debug.Log(_worldGrid.GetWorldPos(endPos,cellCentered: true));    
            Vector3 newPos = Vector3.Lerp(_worldGrid.GetWorldPos(startPos, cellCentered: true),
                                            _worldGrid.GetWorldPos(endPos,cellCentered: true), timeElapsed / lerpDuration);
            transform.position = newPos;
            timeElapsed += Time.deltaTime;
        }
        else if(path.Count != 0)
        {
            transform.position = _worldGrid.GetWorldPos(endPos,cellCentered: true);
            startPos = endPos;
            endPos = path.Dequeue();
            timeElapsed = 0f;
        }
    }

    private bool[,] NewVisitedArray(int n, int m)
    {

        bool[,] visited = new bool[n, m];
    
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                visited[i, j] = false;
            }
        }

        return visited;
    }
    
    bool dfs(Vector2 curPos, ref bool[,] visited)
    {
        int x = (int) curPos.x,
            y = (int) curPos.y;

        if (x < 0 || x >= _worldGrid.Width || y < 0 || y >= _worldGrid.Height) return false;

        if (visited[x, y]) return false;
        
        if (curPos == _worldGrid.WorldPos2GridIdx(target.transform.position))
        {
            Debug.Log($"Wowo! We found the island at position {_worldGrid.GetWorldPos((int) curPos.x, (int) curPos.y, cellCentered: true)}");
            return true;
        }
        
        visited[x, y] = true;

        path.Enqueue(curPos);

        int[,] moves = {{0, 1}, {1,0}, {-1, 0}, {0, -1}};
        
        for (int i = 0; i < 4; i++)
        {
            Vector2 move = new Vector2(moves[i,0], moves[i,1]);
            if (dfs(curPos + move, ref visited)) 
                return true;
        }
        
        return false;
    }
}

