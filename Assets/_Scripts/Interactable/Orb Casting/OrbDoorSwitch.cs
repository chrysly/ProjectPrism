using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OrbDoorSwitch : Interactable {
    [SerializeField] private Material doorMat;
    
    [SerializeField] private Transform displayPoint;
    [SerializeField] private Transform target;

    [SerializeField] private GameObject whiteOrb;
    //TODO: TEMP
    private GameObject activeDisplayOrb;
    private LineRenderer _lineRenderer;

    private void Awake() {
        doorMat.SetFloat("_Radius", 0f);
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.SetPosition(0, displayPoint.position);
        _lineRenderer.SetPosition(1, target.position);
        MaterialPropertyBlock mpb = new();
        _lineRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Alpha", 0f);
        _lineRenderer.SetPropertyBlock(mpb);
    }

    public void Activate() {

        activeDisplayOrb = Instantiate(whiteOrb, displayPoint.position, Quaternion.identity);
        Destroy(activeDisplayOrb.GetComponent<OrbThrow>());
        activeDisplayOrb.transform.DOScale(0.5f, 2f);
        activeDisplayOrb.SetActive(true);
        StartCoroutine(SpawnBeam());
    }

    private IEnumerator SpawnBeam() {
        MaterialPropertyBlock mpb = new();
        _lineRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Alpha", 1f);
        doorMat.DOFloat(11f, "_Radius", 3f);
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
        yield return new WaitForSeconds(0.5f);
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
        yield return new WaitForSeconds(0.05f);
        mpb.SetFloat("_Alpha", 0f);
        _lineRenderer.SetPropertyBlock(mpb);
        yield return null;
    }
}
