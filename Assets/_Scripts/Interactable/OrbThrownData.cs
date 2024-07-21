using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EColor {
    Red, Green, Blue,
    Yellow, Magenta, Cyan,
    White, Black
}

/// <summary>
/// Data container for an object that is thrown (what object, direction, etc)
/// </summary>
public class OrbThrownData 
    {
    private GameObject _orbObject;
    private Vector3 _pushDirection;
    private EColor _color;

    public GameObject OrbObject => _orbObject;
    public Vector3 PushDirection => _pushDirection;
    
    //Added for color identification for togglables
    public EColor Color => _color;

    // constructor
    public OrbThrownData(GameObject otb, Vector3 dir, EColor color) {
        _orbObject = otb;
        _pushDirection = dir;
        _color = color;
    }
}
