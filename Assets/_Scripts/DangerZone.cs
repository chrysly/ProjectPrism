using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Zone that kills player if they are not a certain color
/// </summary>
public class DangerZone : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    public MeshRenderer Meshrender => _mesh;

    private void OnTriggerStay(Collider other) {
        
    }
}

[CustomEditor(typeof(DangerZone))]
public class DangerZoneEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        DangerZone zone = (DangerZone)target;
        if (GUILayout.Button("Color")) {
            MaterialPropertyBlock mpb = new();
            zone.Meshrender.GetPropertyBlock(mpb);
            mpb.SetColor("_BaseColor", Color.yellow);
            zone.Meshrender.SetPropertyBlock(mpb);
        }
    }
}