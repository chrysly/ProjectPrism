using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;

/// <summary>
/// Zone that kills player if they are not a certain color
/// </summary>
public class DangerZone : MonoBehaviour {

    public MeshRenderer Renderer;
    [FormerlySerializedAs("orbColor")] [SerializeField] private EColor eColor;
    public EColor EColor {
        get => eColor;
        set => SetOrbColor(value);
    }

    private void OnTriggerStay(Collider other) {
        
    }

    #if UNITY_EDITOR
    public void SetOrbColor(EColor color) {
        // Update object here;
        eColor = color;
        MaterialPropertyBlock mpb = new();
        Renderer.GetPropertyBlock(mpb);
        Vector4 mColor = color switch {
            EColor.Red => new Vector4(1, 0, 0, 0.35f),
            EColor.Green => new Vector4(0, 1, 0, 0.35f),
            EColor.Blue => new Vector4(0, 0, 1, 0.35f),
            _ => Vector4.one,
        };
        mpb.SetColor("_Color", mColor);
        mpb.SetColor("_BaseColor", mColor);
        
        mColor.Scale(new Vector4(0.35f, 0.35f, 0.35f, 2.857f));
        mpb.SetColor("_EmissionColor", mColor);
        Renderer.SetPropertyBlock(mpb);
    }

    void Reset() => TryGetComponent(out Renderer);
    #endif
}

[CustomEditor(typeof(DangerZone))]
public class DangerZoneEditor : Editor {

    public override void OnInspectorGUI() {
        
        DangerZone dz = (DangerZone) target;

        using (var changeScope = new EditorGUI.ChangeCheckScope()) {
            dz.Renderer = EditorGUILayout.ObjectField("Mesh Renderer", dz.Renderer, 
                                          typeof(MeshRenderer), true) as MeshRenderer;
            GUI.enabled = dz.Renderer != null;
            EColor color = (EColor) EditorGUILayout.EnumPopup("Orb Color", dz.EColor);
            if (dz.EColor != color) dz.EColor = color;
            GUI.enabled = true;
            if (changeScope.changed) EditorUtility.SetDirty(target);
        }
    }
}