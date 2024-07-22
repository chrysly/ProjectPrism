using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bridge : Togglable {
    private MeshRenderer _renderer;
    private BoxCollider _collider;

    private void Awake() {
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponentInChildren<BoxCollider>();
    }

    protected override void Enable(OrbThrownData data) {
        base.Enable(data);
        Material material = new Material(_renderer.materials[0]);
        material.SetColor("_Color", data.OrbObject.GetComponentInChildren<MeshRenderer>().materials[0].GetColor("_Color"));
        material.DOFloat(1.25f, "_Cutoff", 3f);
        Material[] materials = { material };
        _renderer.materials = materials;
        _collider.enabled = true;   //TODO: refactor to dynamic collider toggle, should move with the shader
    }

    protected override void Disable(OrbThrownData data) {
        Material material = new Material(_renderer.materials[0]);
        material.SetColor("_Color", data.OrbObject.GetComponentInChildren<MeshRenderer>().materials[0].GetColor("_Color"));
        material.DOFloat(0f, "_Cutoff", 1.5f);
        Material[] materials = { material };
        _renderer.materials = materials;
        _collider.enabled = false;
    }
}
