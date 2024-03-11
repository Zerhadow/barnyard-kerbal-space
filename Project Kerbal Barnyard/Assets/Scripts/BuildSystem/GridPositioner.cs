using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPositioner : MonoBehaviour
{
    private Vector3 _startingPosition;
    private void Awake()
    {
        _startingPosition = transform.position;
    }
    private void Update()
    {
        //keep grid in same position since parent needs to move
        if(transform.position != _startingPosition)
            transform.position = _startingPosition;
    }
}
