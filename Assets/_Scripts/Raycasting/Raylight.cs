using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Raylight : MonoBehaviour {
    public Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    
    void Reset() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        GenerateRayLight();
        UpdateMesh();
    }

    void GenerateRayLight() {
        vertices = new Vector3[] {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 0)
        };

        triangles = new int[] {
            0, 1, 2
        };
    }

    void UpdateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    public void UpdateRayLight(Vector3 source, Vector3 min, Vector3 max) {
        Mesh rayMesh = GetComponent<MeshFilter>().sharedMesh;
       
         Vector3[] verts = new Vector3[] {
            new Vector3(0, 0, 0), min, max
         };
         
         int[] tris = new int[] {
             0, 1, 2
         };
         
         rayMesh.Clear();
         rayMesh.vertices = verts;
         rayMesh.triangles = tris;
         rayMesh.RecalculateNormals();
         GetComponent<MeshFilter>().sharedMesh = rayMesh;
    }
}
