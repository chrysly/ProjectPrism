using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Zone : MonoBehaviour {
    private BoxCollider _collider;

    protected virtual void Start() {
        _collider = GetComponent<BoxCollider>();
    }

    protected virtual void OnTriggerEnter(Collider other) {
        Debug.Log("Zone entered");
    }

    protected virtual void OnTriggerStay(Collider other) {}
    
    protected virtual void OnTriggerExit(Collider other) {
        Debug.Log("Zone exited");
    }

    protected virtual void OnDrawGizmos() {
        BoxCollider collider = GetComponent<BoxCollider>();
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position + collider.center, collider.size);
    }
}
