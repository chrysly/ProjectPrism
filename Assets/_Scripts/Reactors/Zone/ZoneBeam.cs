using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

[ExecuteInEditMode] [RequireComponent(typeof(MeshFilter))] [RequireComponent(typeof(MeshRenderer))]
public class ZoneBeam : MonoBehaviour {

    private MeshFilter _meshFilter;
    private Mesh _mesh;
    private Zone _zone;
    [SerializeField] private bool dynamicUpdate = false;

    private void Update() {
        if (dynamicUpdate) {
            Initialize(_zone);
            Debug.Log("hii");
        }
    }

    public void Initialize(Zone zone) {
        LinkZone(zone);
        
        _mesh = new Mesh();
        _mesh.name = "Beam";

        _mesh.vertices = GenerateVertices();
        _mesh.triangles = GenerateTriangles();
        
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;
    }

    public void LinkZone(Zone zone) {
        _zone = zone;
    }

    private void OnDrawGizmos() {
        Mesh plane = _zone.GetComponentInChildren<MeshFilter>().sharedMesh;
        Gizmos.color = Color.red;
        Gizmos.DrawCube(plane.vertices[2] + _zone.transform.position, Vector3.one);
        Gizmos.DrawCube(_mesh.vertices[2] + transform.position, Vector3.one);
    }

    private Vector3[] GenerateVertices() {
        Mesh zone = _zone.GetComponentInChildren<MeshFilter>().sharedMesh;
        Vector3 zonePos = _zone.transform.position;
        return new Vector3[] {
            //bottom
            new Vector3(-1, -1, 1),
            new Vector3(1, -1, 1),
            new Vector3(1, -1, -1),
            new Vector3(-1, -1, -1),
            
            //top
            new Vector3(-1, 1, 1),
            zone.vertices[0] + zonePos - transform.position,
            new Vector3(1, 1, -1),
            new Vector3(-1, 1, -1)
        };
    }

    private int[] GenerateTriangles() {
        return new int[] {
            //bottom
            0, 1, 2,
            0, 2, 3,
            
            //top
            4, 5, 6,
            4, 6, 7
        };
    }

    private void OnValidate() {
        
    }
}
