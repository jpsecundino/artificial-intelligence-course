// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
//
// public class CustomGrid
// {
//     public int Width { get; }
//     public int Height { get; }
//     public float CellSize { get; }
//
//     [SerializeField] private Vector2 gridStartOffset = default;
//
//     public int[,] GridArray { get; }
//
//     public CustomGrid(int width, int height, float cellSize, Vector2 gridStartOffset)
//     {
//         this.Width = width;
//         this.Height = height;
//         this.CellSize = cellSize;
//         this.gridStartOffset = gridStartOffset;
//         
//
//         this.GridArray = new int[width, height];
//
//         for (int i = 0; i < this.Width; i++)
//         {
//             for (int j = 0; j < this.Height; j++)
//             {
//                 GridArray[i,j] = 0;
//                 ShowGridCell(i, j);
//                 Debug.Log("asdfasd");
//             }
//         }
//     }
//
//     private void ShowGridCell(int i, int j)
//     {
//         Debug.DrawLine(GetWorldPos(i, j), GetWorldPos(i, j + 1), Color.green, 100f, false);
//         Debug.DrawLine(GetWorldPos(i, j), GetWorldPos(i + 1, j), Color.green, 100f, true);
//         Debug.DrawLine(GetWorldPos(i + 1, j), GetWorldPos(i + 1, j + 1), Color.green, 100f, false);
//         Debug.DrawLine(GetWorldPos(i, j + 1), GetWorldPos(i + 1, j + 1), Color.green, 100f, false);
//     }
//
//     public Vector3 GetCellCenter(Vector2 pos)
//     {
//         return GetCellCenter((int) pos.x, (int) pos.y);
//     }
//     
//     public Vector3 GetWorldPos(Vector2 pos)
//     {
//         return GetWorldPos((int) pos.x, (int) pos.y);
//     }
//
//     public void SetObjective(Vector2 pos)
//     {
//         int x = (int) pos.x,
//             y = (int) pos.y;
//
//         if (x < 0 || x >= Width || y < 0 || y >= Height)
//         {
//             Debug.LogError($"The position for the objective is not valid. \nGridDim ({Width}, {Height}) Pos({x},{y})");
//             return;
//         }
//         
//         GridArray[(int) pos.x, (int) pos.y] = 1;
//     }
//     
//     private Vector3 GetCellCenter(int x, int y)
//     {
//         return GetWorldPos(x,y) + new Vector3(this.CellSize/2, 0f, this.CellSize/2);
//     }
//
//
//     
//
//     
// }
