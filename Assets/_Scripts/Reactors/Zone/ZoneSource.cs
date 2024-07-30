using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ZoneSource : MonoBehaviour {
    [SerializeField] private Zone zone;
    [SerializeField] private Material material;
    [SerializeField] private float sourceSize;
    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, zone.transform.position - transform.position);
        Gizmos.DrawCube(transform.position, Vector3.one * 0.2f);
        Gizmos.DrawCube(zone.transform.position + zone.transform.GetComponent<BoxCollider>().center, Vector3.one * 0.5f);
    }

    public void GenerateBeam() {
        if (transform.GetComponentInChildren<ZoneBeam>() is not null) {
            DestroyImmediate(transform.GetComponentInChildren<ZoneBeam>().gameObject);
        }

        GameObject beam = new GameObject();
        beam.transform.position = transform.position;
        beam.transform.rotation = Quaternion.identity;
        beam.transform.name = "Zone Beam";
        beam.transform.SetParent(transform);
        beam.AddComponent<MeshRenderer>();
        beam.AddComponent<ZoneBeam>().Initialize(zone, sourceSize, material);
    }
}
