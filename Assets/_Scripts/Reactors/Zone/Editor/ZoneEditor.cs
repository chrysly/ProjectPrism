using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ZoneSource))]
public class ZoneEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        ZoneSource handler = (ZoneSource) target;
        if (GUILayout.Button("Generate Beam")) handler.GenerateBeam();
    }
}
