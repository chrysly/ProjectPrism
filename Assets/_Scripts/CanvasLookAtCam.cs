using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attath to a worldspace canvas you want to face the main camera
/// </summary>
public class CanvasLookAtCam : MonoBehaviour
{
    private Transform _t;

    void Start() {
        _t = this.transform;
    }

    void LateUpdate() {
        _t.LookAt(_t.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}
