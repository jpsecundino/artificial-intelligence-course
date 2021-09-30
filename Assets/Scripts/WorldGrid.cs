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
    
    
    private static WorldGrid _instance;
    public static WorldGrid Instance { get { return _instance; } }
        
    
    // Start is called before the first frame update\
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;    
    
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
    
    private void ShowGridCell(int i, int j)
    {
        Debug.DrawLine(GetWorldPos(i, j), GetWorldPos(i, j + 1), Color.green, 100f, false);
        Debug.DrawLine(GetWorldPos(i, j), GetWorldPos(i + 1, j), Color.green, 100f, true);
        Debug.DrawLine(GetWorldPos(i + 1, j), GetWorldPos(i + 1, j + 1), Color.green, 100f, false);
        Debug.DrawLine(GetWorldPos(i, j + 1), GetWorldPos(i + 1, j + 1), Color.green, 100f, false);
    }

    private Vector3 GetWorldPos(int x, int y)
    {
        return new Vector3(x,0f ,y) * cellSize;
    }

    public Vector2 WorldPos2GridIdx(Vector2 pos)
    {
        int x = Mathf.RoundToInt(pos.x / cellSize),
            y = Mathf.RoundToInt(pos.y / cellSize);

        x = (int) Mathf.Clamp(x, 0, Width - 1);
        y = (int) Mathf.Clamp(y, 0, Height - 1);
        
        return new Vector2(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2);
    }
}
