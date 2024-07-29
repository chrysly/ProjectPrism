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

        MaterialPropertyBlock mpb = new();
        _renderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Cutoff", -0.6f);
        _renderer.SetPropertyBlock(mpb);
    }

    protected override void Enable(OrbThrownData data) {
        base.Enable(data);
        Material material = new Material(_renderer.materials[0]);
        material.SetColor("_Color", data.Color.GetColor());
        //material.DOFloat(1.25f, "_Cutoff", 3f);
        StartCoroutine(UpdateCutoff(0.35f, 1.5f));
        Material[] materials = { material };
        _renderer.materials = materials;
        _collider.enabled = true;   //TODO: refactor to dynamic collider toggle, should move with the shader
    }

    protected override void Disable(OrbThrownData data) {
        Material material = new Material(_renderer.materials[0]);
        material.SetColor("_Color", data.Color.GetColor());
        //material.DOFloat(0f, "_Cutoff", 1.5f);
        StartCoroutine(UpdateCutoff(-0.6f, 0.75f));
        Material[] materials = { material };
        _renderer.materials = materials;
        _collider.enabled = false;
    }

    private IEnumerator UpdateCutoff(float target, float time) {
        MaterialPropertyBlock mpb = new();
        _renderer.GetPropertyBlock(mpb);
        float initialVal = mpb.GetFloat("_Cutoff");
        float lerpValue = 0;
        while (lerpValue < 1) {
            lerpValue = Mathf.MoveTowards(lerpValue, 1, Time.deltaTime * time);
            mpb.SetFloat("_Cutoff", Mathf.Lerp(initialVal, target, lerpValue));
            _renderer.SetPropertyBlock(mpb);
            yield return null;
        }
    }
}
