using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

[ExecuteInEditMode] [RequireComponent(typeof(MeshFilter))] [RequireComponent(typeof(MeshRenderer))]
public class ZoneBeam : MonoBehaviour {

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private Mesh _mesh;
    private Zone _zone;
    private float _size;
    private Material _material;
    [SerializeField] private bool dynamicUpdate = false;

    private void Update() {
        if (dynamicUpdate && _size > 0) {
            UpdateMesh(_zone);
            Debug.Log("hii");
        }
    }

    public void Initialize(Zone zone, float size, Material material) {
        _size = size;
        LinkZone(zone);
        
        _mesh = new Mesh();
        _mesh.name = "Beam";

        _mesh.vertices = GenerateVertices();
        _mesh.triangles = GenerateTriangles();
        
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.sharedMaterials = new[] { new Material(material) };   //yes i know no it is not permanent
        _meshFilter.mesh = _mesh;
    }
    
    public void UpdateMesh(Zone zone) {
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

    // private void OnDrawGizmos() {
    //     Mesh plane = _zone.GetComponentInChildren<MeshFilter>().sharedMesh;
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawCube(plane.vertices[10] + _zone.transform.position, Vector3.one);
    //     Gizmos.DrawCube(_mesh.vertices[2] + transform.position, Vector3.one);
    // }

    private Vector3[] GenerateVertices() {
        Mesh zone = _zone.GetComponentInChildren<MeshFilter>().sharedMesh;
        Vector3 zonePos = _zone.transform.position;
        Vector3 pos = transform.position;
        return new Vector3[] {
            //bottom
            zone.vertices[10] + zonePos - pos,
            zone.vertices[0] + zonePos - pos,
            zone.vertices[110] + zonePos - pos,
            zone.vertices[120] + zonePos - pos,
            
            //top
            new Vector3(-1, 1, 1) * _size,
            new Vector3(1, 1, 1) * _size,
            new Vector3(1, 1, -1) * _size,
            new Vector3(-1, 1, -1) * _size,
            
            //left
            zone.vertices[10] + zonePos - pos,
            zone.vertices[120] + zonePos - pos,
            new Vector3(-1, 1, 1) * _size,
            new Vector3(-1, 1, -1) * _size,
            
            //right
            zone.vertices[0] + zonePos - pos,
            zone.vertices[110] + zonePos - pos,
            new Vector3(1, 1, 1) * _size,
            new Vector3(1, 1, -1) * _size,
            
            //front
            zone.vertices[110] + zonePos - pos,
            zone.vertices[120] + zonePos - pos,
            new Vector3(1, 1, -1) * _size,
            new Vector3(-1, 1, -1) * _size,
            
            //back
            zone.vertices[10] + zonePos - pos,
            zone.vertices[0] + zonePos - pos,
            new Vector3(-1, 1, 1) * _size,
            new Vector3(1, 1, 1) * _size
        };
    }

    private int[] GenerateTriangles() {
        return new int[] {
            //bottom
            1, 0, 2,
            2, 0, 3,
            //top
            4, 5, 6,
            4, 6, 7,
            
            //left
            9, 10, 11,
            8, 10, 9,
            //right
            12, 13, 15,
            14, 12, 15,
            
            //front
            16,17,19,
            18,16,19,
            //back
            20, 21, 23,
            22, 20, 23
        };
    }

    private void OnValidate() {
        
    }
}
