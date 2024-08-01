using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OrbCaster : Interactable {
    [SerializeField] private Transform displayPoint;
    [SerializeField] private List<Togglable> togglables;
    [SerializeField] private Transform target;
    //TODO: TEMP
    [SerializeField] private bool primer;
    [SerializeField] private OrbDoorSwitch doorSwitch;
    private GameObject activeDisplayOrb;
    private LineRenderer _lineRenderer;

    private void Awake() {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.SetPosition(0, displayPoint.position);
        _lineRenderer.SetPosition(1, target.position);
        MaterialPropertyBlock mpb = new();
        _lineRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Alpha", 0f);
        _lineRenderer.SetPropertyBlock(mpb);
    }

    public override void InteractAction(OrbThrownData data ) {
        base.InteractAction(data);
        if (activeDisplayOrb != null) {
            Destroy(activeDisplayOrb);
        }

        activeDisplayOrb = Instantiate(data.OrbObject, displayPoint.position, Quaternion.identity);
        Destroy(activeDisplayOrb.GetComponent<OrbThrow>());
        activeDisplayOrb.transform.DOScale(0.3f, 1f);
        activeDisplayOrb.SetActive(true);
        
        CheckTogglables(data);
        StartCoroutine(SpawnBeam());
    }

    private IEnumerator SpawnBeam() {
        MaterialPropertyBlock mpb = new();
        _lineRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Alpha", 1f);
        _lineRenderer.SetPropertyBlock(mpb);
        yield return new WaitForSeconds(0.1f);
        mpb.SetFloat("_Alpha", 0f);
        _lineRenderer.SetPropertyBlock(mpb);
        yield return new WaitForSeconds(0.1f);
        mpb.SetFloat("_Alpha", 1f);
        _lineRenderer.SetPropertyBlock(mpb);
        yield return new WaitForSeconds(0.05f);
        mpb.SetFloat("_Alpha", 0f);
        _lineRenderer.SetPropertyBlock(mpb);
        yield return new WaitForSeconds(0.05f);
        mpb.SetFloat("_Alpha", 1f);
        _lineRenderer.SetPropertyBlock(mpb);

        if (primer) {
            doorSwitch.Activate();
            doorSwitch.transform.GetChild(1).DOShakeScale(2f, new Vector3(2f, 2f, 2f));
        }
        yield return null;
    }

    private void CheckTogglables(OrbThrownData data) {
        foreach (Togglable togglable in togglables) {
            togglable.Toggle(data);
        }
    }
}