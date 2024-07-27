using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RaycastVisualizer : MonoBehaviour {
    [SerializeField] private float rayMagnitude = 10f;
    [SerializeField] [Range(0f, 180f)] private float raySpreadAngle = 0f;
    [SerializeField] private Raylight rayLight;

    private Vector3 minRay;
    private Vector3 maxRay;
    
    private Vector3 prevRotation = Vector3.zero;
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.black;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * rayMagnitude;
        Gizmos.DrawRay(transform.position, direction);
        
        minRay = Quaternion.AngleAxis(raySpreadAngle, Vector3.up) * direction;
        maxRay = Quaternion.AngleAxis(-raySpreadAngle, Vector3.up) * direction;

        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, minRay);
        Gizmos.DrawRay(transform.position, maxRay);

    }

    private void Update() {
        GetComponent<Raylight>().UpdateRayLight(transform.position, maxRay, minRay); 
    }
}
