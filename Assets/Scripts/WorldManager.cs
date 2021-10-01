using UnityEngine;


public class WorldManager: MonoBehaviour
{
    private static WorldGrid _worldGrid;
    // Start is called before the first frame update

    private const char WALL = '#';
    private const char TARGET = 't';
    private const char START = 's';

    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject boat;
    [SerializeField] private GameObject target;
    
    private void Start()
    {
        _worldGrid = WorldGrid.Instance;
        ReadWorld();
        PrepareWorld();
    }

    private void PrepareWorld()
    {
        for (int i = 0; i < _worldGrid.width; i++)
        {
            for (int j = 0; j < _worldGrid.height; j++)
            {
                if (_worldGrid.gridMap[i, j] == WALL)
                {
                    Instantiate(obstacle, _worldGrid.GetWorldPos(i, j), Quaternion.identity);
                }else if (_worldGrid.gridMap[i, j] == TARGET)
                {
                    Instantiate(target, _worldGrid.GetWorldPos(i, j), Quaternion.identity);
                }
                else if (_worldGrid.gridMap[i, j] == START)
                {
                    Instantiate(boat, _worldGrid.GetWorldPos(i, j), Quaternion.identity);
                }
            }
        }
    }

    private void ReadWorld()
    {
        
        string[] lines = System.IO.File.ReadAllLines("Assets/Scripts/Pathfinding/Maps/1/map.txt");
        string[] gridDimensions = lines[0].Split(' ');
        _worldGrid.width = int.Parse(gridDimensions[0]);
        _worldGrid.height = int.Parse(gridDimensions[1]);
        
        Debug.Log(_worldGrid.width + " " + _worldGrid.height);
        
        _worldGrid.gridMap = new char[_worldGrid.width, _worldGrid.height];
                
        for(int i = 1; i < lines.Length; i++)
        {
            string line = "";
            for (int j = 0; j < lines[i].Length; j++)
            {
                _worldGrid.gridMap[i - 1, j] = lines[i][j];
        
                line += _worldGrid.gridMap[i - 1, j];
            }
                    
            Debug.Log(line);
        }
        
    }
    
    
}
