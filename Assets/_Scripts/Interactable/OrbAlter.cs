using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

//TODO: Refactor to "Sender" class
public class OrbAlter : Interactable {
    [SerializeField] private Transform displayPoint;
    [SerializeField] private MeshRenderer pillar;
    [SerializeField] private List<Togglable> togglables;    //The theory is that a list of togglables will be taken in that activate when an orb of a certain color interacts

    /// PLACEHOLDER VARIABLES;
    [SerializeField] private EColor targetColor;
    [SerializeField] private Animator planeAnim;
    public Transform path;
    public Animator pathAnimator;
    private MeshRenderer currOrbRenderer;
    private EColor eColor;

    private GameObject activeDisplayOrb;
    
    //Uhhh yea this is pretty hardcoded for demo sake lmaoo
    public override void InteractAction(OrbThrownData data ) {
        ///base.InteractAction(data);
        EColor eColor = activeDisplayOrb == null ? data.Color : data.Color.Add(this.eColor);
        Color orbColor = eColor.GetColor();
        if (activeDisplayOrb == null) {
            activeDisplayOrb = Instantiate(data.OrbObject, displayPoint.position, Quaternion.identity);
            Destroy(activeDisplayOrb.GetComponent<OrbThrow>());
            activeDisplayOrb.transform.DOScale(1f, 1f);
            activeDisplayOrb.SetActive(true);
            currOrbRenderer = activeDisplayOrb.GetComponentInChildren<MeshRenderer>(true);
            this.eColor = data.Color;

            Material material = new Material(pillar.materials[0]);
            material.DOColor(orbColor  * 1.9f, "_Circuit_Color", 1f);
            Material[] materials = { material };
            pillar.materials = materials;
            CheckTogglables(new OrbThrownData(data.OrbObject, data.PushDirection, eColor));
        } else {
            StartCoroutine(DOColor(orbColor, new OrbThrownData(data.OrbObject, data.PushDirection, eColor)));
        }
    }

    private void CheckTogglables(OrbThrownData data) {
        foreach (Togglable togglable in togglables) {
            togglable.Toggle(data);
        }
    }

    private IEnumerator DOColor(Color targetColor, OrbThrownData data) {
        bool colorMatch = targetColor == this.targetColor.GetColor();
        targetColor.a = 0;
        Vector4 currColor = eColor.GetColor();
        float lerpVal = 0;
        if (colorMatch) planeAnim.SetTrigger("Blink");
        while (lerpVal < 1) {
            lerpVal = Mathf.MoveTowards(lerpVal, 1, Time.deltaTime * 2);
            MaterialPropertyBlock mpb = new();
            pillar.GetPropertyBlock(mpb);
            Vector4 nColor = Vector4.Lerp(currColor, targetColor, lerpVal);
            mpb.SetVector("_Circuit_Color", nColor * 1.9f);
            pillar.SetPropertyBlock(mpb);
            mpb = new();
            currOrbRenderer.GetPropertyBlock(mpb);
            mpb.SetVector("_Color", nColor * 2.2f);
            currOrbRenderer.SetPropertyBlock(mpb);
            yield return null;
        } if (colorMatch) CheckTogglables(data);
    }
}
