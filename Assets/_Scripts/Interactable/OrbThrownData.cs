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
    private OrbColor _color;

    public enum OrbColor {
        Red,
        Green,
        Blue
    }

    public GameObject OrbObject => _orbObject;
    public Vector3 PushDirection => _pushDirection;
    
    //Added for color identification for togglables
    public OrbColor Color => _color;

    // constructor
    public OrbThrownData(GameObject otb, Vector3 dir, OrbColor color) {
        _orbObject = otb;
        _pushDirection = dir;
        _color = color;
    }
}
