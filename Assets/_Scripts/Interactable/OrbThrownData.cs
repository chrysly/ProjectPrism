using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data container for an object that is thrown (what object, direction, etc)
/// </summary>
public class OrbThrownData 
    {
    private GameObject _orbObject;
    private Vector3 _pushDirection;

    public GameObject OrbObject => _orbObject;
    public Vector3 PushDirection => _pushDirection;

    // constructor
    public OrbThrownData(GameObject otb, Vector3 dir) {
        _orbObject = otb;
        _pushDirection = dir;
    }
}
