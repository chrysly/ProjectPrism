using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EColor : uint {
    Black = 0,      /// 0 0 0
    Blue = 1,       /// 0 0 1
    Green = 2,      /// 0 1 0
    Cyan = 3,       /// 0 1 1
    Red = 4,        /// 1 0 0
    Magenta = 5,    /// 1 0 1
    Yellow = 6,     /// 1 1 0
    White = 7,      /// 1 1 1
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
