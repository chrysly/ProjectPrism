using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ZoneSource : MonoBehaviour {
    [SerializeField] private Zone zone;

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, zone.transform.position - transform.position);
        Gizmos.DrawCube(transform.position, Vector3.one * 0.2f);
        Gizmos.DrawCube(zone.transform.position + zone.transform.GetComponent<BoxCollider>().center, Vector3.one * 0.5f);
    }
}
