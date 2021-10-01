using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WorldGrid : MonoBehaviour
{
    
    [SerializeField] private float cellSize;
    public int Width;
    public int Height;
    private char[,] gridMap;
    
    private static WorldGrid _instance;
    public static WorldGrid Instance { get { return _instance; } }
        
    
    // Start is called before the first frame update\
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            PrepareWorld();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        
        for (int i = 0; i < this.Width; i++)
        {
            for (int j = 0; j < this.Height; j++)
            {
                ShowGridCell(i, j);
            }
        }
    }

    private void PrepareWorld()
    {

        string[] lines = System.IO.File.ReadAllLines("Assets/Scripts/Pathfinding/Maps/1/map.txt");
        string[] gridDimensions = lines[0].Split(' ');
        Width = int.Parse(gridDimensions[0]);
        Height = int.Parse(gridDimensions[1]);
        Debug.Log(Width + " " + Height);
        gridMap = new char[Width, Height];
        
        for(int i = 1; i < lines.Length; i++)
        {
            string line = "";
            for (int j = 0; j < lines[i].Length; j++)
            {
                gridMap[i - 1, j] = lines[i][j];

                line += gridMap[i - 1, j];
            }
            
            Debug.Log(line);
        }
    }

    private void ShowGridCell(int i, int j)
    {
        Debug.DrawLine(GetWorldPos(i, j), GetWorldPos(i, j + 1), Color.green, 100f, true);
        Debug.DrawLine(GetWorldPos(i, j), GetWorldPos(i + 1, j), Color.green, 100f, true);
        Debug.DrawLine(GetWorldPos(i + 1, j), GetWorldPos(i + 1, j + 1), Color.green, 100f, true);
        Debug.DrawLine(GetWorldPos(i, j + 1), GetWorldPos(i + 1, j + 1), Color.green, 100f, true);
    }

    public Vector3 GetWorldPos(Vector2 pos, bool cellCentered = false)
    {
        return GetWorldPos((int) pos.x, (int) pos.y, cellCentered);
    }
    
    public Vector3 GetWorldPos(int x, int y, bool cellCentered = false)
    {
        Vector3 pos = new Vector3(x, 0f, y) * cellSize;
        
        if (cellCentered)
        {
            pos += new Vector3(cellSize / 2, 0f, cellSize / 2);
        }
        
        return pos;
    }

    public Vector2 WorldPos2GridIdx(Vector3 pos)
    {
        int x = (int) (pos.x / cellSize),
            y = (int) (pos.z / cellSize);

        x = (int) Mathf.Clamp(x, 0, Width - 1);
        y = (int) Mathf.Clamp(y, 0, Height - 1);
        
        return new Vector2(x , y);
    }
    
    public Vector2 WorldPos2GridPos(Vector3 pos)
    {
        int x = (int) (pos.x / cellSize),
            y = (int) (pos.z / cellSize);

        x = (int) Mathf.Clamp(x, 0, Width - 1);
        y = (int) Mathf.Clamp(y, 0, Height - 1);
        
        return new Vector2(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2);
    }
}
