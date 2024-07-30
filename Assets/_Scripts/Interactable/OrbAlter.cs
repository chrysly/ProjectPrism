using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.VFX;

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
    private VisualEffect shineFX, sparkFX;
    [SerializeField] private OrbColorData colorData;

    private GameObject activeDisplayOrb;
    
    //Uhhh yea this is pretty hardcoded for demo sake lmaoo
    public override void InteractAction(OrbThrownData data ) {
        ///base.InteractAction(data);
        EColor eColor = activeDisplayOrb == null ? data.Color : data.Color.Add(this.eColor);
        Color orbColor = eColor.GetColor();
        Destroy(data.OrbObject);
        if (activeDisplayOrb == null) {
            activeDisplayOrb = Instantiate(data.OrbObject, displayPoint.position, Quaternion.identity);
            if (activeDisplayOrb.TryGetComponent(out OrbThrow orbThrow)) {
                shineFX = orbThrow.shineFX;
                sparkFX = orbThrow.sparkFX;
                Destroy(orbThrow);
            }
            if (activeDisplayOrb.TryGetComponent(out Rigidbody rb)) Destroy(rb);
            activeDisplayOrb.transform.DOScale(0.6f, 1f);
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
            if (togglable) togglable.Toggle(data);
        }
    }

    private IEnumerator DOColor(Color targetColor, OrbThrownData data) {
        bool colorMatch = targetColor == this.targetColor.GetColor();
        Gradient gradShineInit = shineFX.GetGradient("Gradient Color"),
                 gradSparkInit = sparkFX.GetGradient("Gradient");
        Gradient gradBeamTarget = data.Color == EColor.Red ? colorData.rLightBeamGradient
                                : data.Color == EColor.Green ? colorData.gLightBeamGradient
                                : data.Color == EColor.Yellow ? colorData.yLightBeamGradient
                                : null;
        Gradient gradSparkTarget = data.Color == EColor.Red ? colorData.rSparksGradient
                                 : data.Color == EColor.Green ? colorData.gSparksGradient
                                 : data.Color == EColor.Yellow ? colorData.ySparksGradient
                                 : null;

        targetColor.a = 0;
        Vector4 currColor = eColor.GetColor();
        float lerpVal = 0;
        if (colorMatch) planeAnim.SetTrigger("Blink");
        while (lerpVal < 1) {
            if (gradBeamTarget != null) {
                shineFX.SetGradient("Gradient Color", LerpGradient(gradShineInit, gradBeamTarget, lerpVal));
                sparkFX.SetGradient("Gradient", LerpGradient(gradSparkInit, gradSparkTarget, lerpVal));
                sparkFX.SetGradient("Color", LerpGradient(gradSparkInit, gradSparkTarget, lerpVal));
            } lerpVal = Mathf.MoveTowards(lerpVal, 1, Time.deltaTime * 2);
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

    private Gradient LerpGradient(Gradient a, Gradient b, float t) {
        var keysTimes = new List<float>();

        for (int i = 0; i < a.colorKeys.Length; i++) {
            float k = a.colorKeys[i].time;
            if (!keysTimes.Contains(k))
                keysTimes.Add(k);
        }

        for (int i = 0; i < b.colorKeys.Length; i++) {
            float k = b.colorKeys[i].time;
            if (!keysTimes.Contains(k))
                keysTimes.Add(k);
        }

        for (int i = 0; i < a.alphaKeys.Length; i++) {
            float k = a.alphaKeys[i].time;
            if (!keysTimes.Contains(k))
                keysTimes.Add(k);
        }

        for (int i = 0; i < b.alphaKeys.Length; i++) {
            float k = b.alphaKeys[i].time;
            if (!keysTimes.Contains(k))
                keysTimes.Add(k);
        }

        GradientColorKey[] clrs = new GradientColorKey[keysTimes.Count];
        GradientAlphaKey[] alphas = new GradientAlphaKey[keysTimes.Count];

        for (int i = 0; i < keysTimes.Count; i++) {
            float key = keysTimes[i];
            var clr = Color.Lerp(a.Evaluate(key), b.Evaluate(key), t);
            clrs[i] = new GradientColorKey(clr, key);
            alphas[i] = new GradientAlphaKey(clr.a, key);
        }

        Gradient g = new();
        g.SetKeys(clrs, alphas);
        return g;
    }
}
