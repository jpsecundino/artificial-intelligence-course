// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Security.AccessControl;
// using UnityEngine;
//
// public class Finder : MonoBehaviour
// {
//
//     [SerializeField]
//     private Vector2 _objectivePos;
//
//     public GameObject target;
//     
//     private WorldGrid _worldGrid;
//     private List<Vector2> path;
//     private bool[,] visited;
//
//     private void Awake()
//     {
//         // InitializeVisitedArray();
//     }
//
//     private void Start()
//     {
//         _worldGrid = WorldGrid.Instance;
//         // dfs(transform.position);
//     }
//
//     private void InitializeVisitedArray()
//     {
//         int mapWidth = _worldGrid.Width,
//             mapHeight = _worldGrid.Height;
//     
//         visited = new bool[mapWidth, mapHeight];
//     
//         for (int i = 0; i < mapWidth; i++)
//         {
//             for (int j = 0; j < mapHeight; j++)
//             {
//                 visited[i, j] = false;
//             }
//         }
//     }
//
//     // void dfs(Vector2 curPos)
//     // {
//     //
//     //     int x = (int) curPos.x,
//     //         y = (int) curPos.y;
//     //     
//     //     if (x < 0 || x >= _worldGrid.Width || y < 0 || y >= _worldGrid.Height) return;
//     //
//     //     
//     //     
//     //     if (visited[x, y]) return;
//     //     
//     //     
//     //         
//     //     int[,] moves = {{0, 1}, {1,0}, {-1, 0}, {0, -1}};
//     //
//     //     for (int i = 0; i < moves.Length; i++)
//     //     {
//     //         
//     //     }
//     // }
// }
//
