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
    private Queue<Vector2> _path;
    
    private float _timeElapsed;
    private float _posLerpDuration;

    private Vector2 _startPos;
    private Vector2 _endPos;

    private void Start()
    {
        GetComponent<DiscretePositioning>().enabled = false;
        
        _worldGrid = WorldGrid.Instance;
        
        FindTarget();
        
        _posLerpDuration = travelTime / _path.Count;
        _startPos = _path.Dequeue();
        _endPos = _path.Dequeue();

    }

    private void FindTarget()
    {
        bool[,] visited = NewVisitedArray(_worldGrid.Width, _worldGrid.Height);

        _path = new Queue<Vector2>();

        Vector2 curPos = _worldGrid.WorldPos2GridIdx(transform.position);

        dfs(curPos, ref visited);
    }

    void Update()
    {
        
        if (_timeElapsed < _posLerpDuration)
        {
            Debug.Log(_worldGrid.GetWorldPos(_endPos,cellCentered: true));    
            Vector3 newPos = Vector3.Lerp(_worldGrid.GetWorldPos(_startPos, cellCentered: true),
                                            _worldGrid.GetWorldPos(_endPos,cellCentered: true), _timeElapsed / _posLerpDuration);
            transform.position = newPos;
            _timeElapsed += Time.deltaTime/1.1f;
        }
        else if(_path.Count != 0)
        {
            transform.position = _worldGrid.GetWorldPos(_endPos,cellCentered: true);
            _startPos = _endPos;
            _endPos = _path.Dequeue();
            transform.LookAt(_worldGrid.GetWorldPos(_endPos,cellCentered: true));
            _timeElapsed = 0f;
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

        _path.Enqueue(curPos);

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

