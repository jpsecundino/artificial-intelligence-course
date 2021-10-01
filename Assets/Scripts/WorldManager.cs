using UnityEngine;


public class WorldManager : MonoBehaviour
{

    private const char WALL = '#';
    private const char TARGET = 't';
    private const char START = 's';
    
    // Start is called before the first frame update
    [SerializeField]
    public enum Method
    {
        DFS,
        AStar
    } 
    
    public enum Map
    {
        A,
        B,
        C
    }
    
    private static WorldManager _instance;
    public static WorldManager Instance { get { return _instance; } }
    
    private static WorldGrid _worldGrid;

    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject boat;
    [SerializeField] private GameObject target;
    [SerializeField] public Method _method;
    [SerializeField] public Map _map;
    
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
        
    }
    
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
                    Instantiate(obstacle, _worldGrid.GetWorldPos(i, j, cellCentered:true), Quaternion.identity);
                }else if (_worldGrid.gridMap[i, j] == TARGET)
                {
                    Instantiate(target, _worldGrid.GetWorldPos(i, j, cellCentered:true), Quaternion.identity);
                }
                else if (_worldGrid.gridMap[i, j] == START)
                {
                    Instantiate(boat, _worldGrid.GetWorldPos(i, j, cellCentered:true), Quaternion.identity);
                }
            }
        }
    }

    private void ReadWorld()
    {
        
        string[] lines = System.IO.File.ReadAllLines($"Assets/Scripts/Pathfinding/Maps/{_map}/map.txt");
        string[] gridDimensions = lines[0].Split(' ');
        _worldGrid.width = int.Parse(gridDimensions[0]);
        _worldGrid.height = int.Parse(gridDimensions[1]);
        
        Debug.Log(_worldGrid.width + " " + _worldGrid.height);
        
        _worldGrid.gridMap = new char[_worldGrid.width, _worldGrid.height];
                
        for(int i = 1; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                _worldGrid.gridMap[i - 1, j] = lines[i][j];
                
            }
            
        }
        
    }
    
    
}
