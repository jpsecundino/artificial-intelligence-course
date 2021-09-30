using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DiscretePositioning : MonoBehaviour
{
    private WorldGrid _worldGrid;

    private void Start()
    {
        _worldGrid = WorldGrid.Instance;
    }

    void Update()
    {
        Vector2 newPosIdx = _worldGrid.WorldPos2GridIdx(new Vector2(transform.position.x, transform.position.z));
        transform.position = new Vector3(newPosIdx.x, 0f, newPosIdx.y);
    }
}
