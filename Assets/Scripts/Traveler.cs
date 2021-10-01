
using System.Collections.Generic;
using UnityEngine;

public class Traveler : MonoBehaviour
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
        //find another place to put this
        GetComponent<DiscretePositioning>().enabled = false;
        
        _worldGrid = WorldGrid.Instance;

        ReadPath(); 
        
        _posLerpDuration = travelTime / _path.Count;
        _startPos = _path.Dequeue();
        _endPos = _path.Dequeue();

    }

    private void ReadPath()
    {
        _path = new Queue<Vector2>();
        string[] lines = System.IO.File.ReadAllLines($"Assets/Scripts/Pathfinding/Maps/{WorldManager.Instance._map}/searchTree{WorldManager.Instance._method}.txt");
        foreach (string line in lines)
        {
            string[] coordinates = line.Split(' ');
            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);
            _path.Enqueue(new Vector2(x,y));
        }
        
    }

    void Update()
    {
        if (_timeElapsed < _posLerpDuration)
        {
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

}

