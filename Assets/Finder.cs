using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class Finder : MonoBehaviour
{
    public GameObject target;
    
    private WorldGrid _worldGrid;
    private List<Vector2> path;

    private void Start()
    {
        _worldGrid = WorldGrid.Instance;
        bool[,] visited = NewVisitedArray(_worldGrid.Width, _worldGrid.Height);
        path = new List<Vector2>();
        
        dfs(_worldGrid.WorldPos2GridIdx(transform.position), ref visited);
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

    void dfs(Vector2 curPos, ref bool[,] visited)
    {
        int x = (int) curPos.x,
            y = (int) curPos.y;

        if (x < 0 || x >= _worldGrid.Width || y < 0 || y >= _worldGrid.Height) return;

        if (visited[x, y]) return;
        
        if (curPos == _worldGrid.WorldPos2GridIdx(target.transform.position))
        {
            Debug.Log($"Wowo! We found the island at position {_worldGrid.GetWorldPos((int) curPos.x, (int) curPos.y, cellCentered: true)}");
            return;
        }
        
        visited[x, y] = true;
        
        // System.Threading.Thread.Sleep(50);
        
        path.Add(curPos);

        int[,] moves = {{0, 1}, {1,0}, {-1, 0}, {0, -1}};
        
        for (int i = 0; i < 4; i++)
        {
            Vector2 move = new Vector2(moves[i,0], moves[i,1]);
            dfs(curPos + move, ref visited);
            path.Add(curPos);
        }
    }
}

