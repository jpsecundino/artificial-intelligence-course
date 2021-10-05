
using System.Collections.Generic;
using UnityEngine;

public class Traveler : MonoBehaviour
{
    public float travelTime;
    public GameObject visitedBlock;
    private WorldGrid _worldGrid;
    private List<Vector2> _truePath;
    private List<Vector2> _searchTree;
    
    private float _timeElapsed;
    private float _posLerpDuration;
    public float _searchLerpDuration = 0.00001f;

    private Vector2 _startPos;
    private Vector2 _endPos;
    private bool truePath = false;
    public float speed = 10f;

    private void Start()
    {
        //find another place to put this
        GetComponent<DiscretePositioning>().enabled = false;
        
        _worldGrid = WorldGrid.Instance;

        ReadPath(); 
        
        _posLerpDuration = travelTime / _truePath.Count;
        if(!truePath) {
            _startPos = _searchTree[0];
            _searchTree.RemoveAt(0);
            _endPos = _searchTree[0];
            _searchTree.RemoveAt(0);
        }else {
            _startPos = _truePath[0];
            _truePath.RemoveAt(0);
            _endPos = _truePath[0];
            _truePath.RemoveAt(0);
        }
    }

    private void ReadPath()
    {
        _truePath = new List<Vector2>();
        _searchTree = new List<Vector2>();
        bool isTruePath = false;
        string[] lines = System.IO.File.ReadAllLines($"Assets/Scripts/Pathfinding/Maps/{WorldManager.Instance._map}/searchTree{WorldManager.Instance._method}.txt");
        foreach (string line in lines)
        {
            if(line == "tp") {
                isTruePath = true;
            }else {
                string[] coordinates = line.Split(' ');
                int x = int.Parse(coordinates[0]);
                int y = int.Parse(coordinates[1]);
                if(!isTruePath) {
                    _searchTree.Add(new Vector2(x,y));
                }else {
                    _truePath.Add(new Vector2(x,y));
                }
            }
        }
        
    }

    void Update()
    {
        if(truePath) {
            FollowTruePath();
        }else {
            FollowSearchTree();
        }
    }

    void FollowTruePath() {
        if (_timeElapsed < _posLerpDuration)
        {
            Vector3 newPos = Vector3.Lerp(_worldGrid.GetWorldPos(_startPos, cellCentered: true),
                                            _worldGrid.GetWorldPos(_endPos,cellCentered: true), _timeElapsed / _posLerpDuration);
            transform.position = newPos;
            _timeElapsed += Time.deltaTime/1.1f;
            
        }
        else if(_truePath.Count != 0)
        {
            transform.position = _worldGrid.GetWorldPos(_endPos,cellCentered: true);
            _startPos = _endPos;
            _endPos = _truePath[0];
            _truePath.RemoveAt(0);
            transform.LookAt(_worldGrid.GetWorldPos(_endPos,cellCentered: true));
            _timeElapsed = 0f;
        }
    }

    void FollowSearchTree() {
        /*if (_timeElapsed < _searchLerpDuration)
        {
            Vector3 newPos = Vector3.Lerp(_worldGrid.GetWorldPos(_startPos, cellCentered: true),
                                            _worldGrid.GetWorldPos(_endPos,cellCentered: true), _timeElapsed / _searchLerpDuration);
            _timeElapsed += Time.deltaTime*speed;
        
            
        }
        else */if(_searchTree.Count != 0)
        {
            Instantiate(visitedBlock, _worldGrid.GetWorldPos(_endPos,cellCentered: true), Quaternion.identity);
            _startPos = _endPos;
            _endPos = _searchTree[0];
            _searchTree.RemoveAt(0);
            _timeElapsed = 0f;
        }
        else {
            truePath = true;
            Debug.Log(_truePath.Count);
            _startPos = _truePath[0];
            _truePath.RemoveAt(0);
            _endPos = _truePath[0];
            _truePath.RemoveAt(0);
        }
    }

}

