using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightComponent : MonoBehaviour {
    [Header("Player Attributes")] 
    [SerializeField] private ColorDriver driver;
    
    [Header("Spotlight Attributes")]
    [SerializeField] private Renderer spotlight;
    [SerializeField] private float sIntensity;
    
    [Header("Ringlight Attributes")]
    [SerializeField] private Renderer ringLight;
    [SerializeField] private float rIntensity;
    [SerializeField] private Light lightComponent;
    [SerializeField] private float lerpDuration = 0.5f;

    private void Awake() {
        driver.OnColorSwap += SwapColor;
    }

    private void SwapColor(Vector4 color) {
        StartCoroutine(ColorSwapAction(color));
    }

    private IEnumerator ColorSwapAction(Vector4 color) {
        float lerpVal = 0f;
        MaterialPropertyBlock mpb = new();
        spotlight.SetPropertyBlock(mpb);
        Vector4 currSColor = mpb.GetColor("_Color");
        mpb = new();
        ringLight.SetPropertyBlock(mpb);
        Vector4 currRColor = mpb.GetColor("_Color");
        Vector4 currLightColor = lightComponent.color;
        
        while (lerpVal < lerpDuration) {
            mpb = new();
            lerpVal = Mathf.MoveTowards(lerpVal, lerpDuration, Time.deltaTime * 2);
            spotlight.GetPropertyBlock(mpb);
            Vector4 sColor = Vector4.Lerp(currSColor, color, lerpVal);
            mpb.SetVector("_Color", sColor * Mathf.Pow(2f, sIntensity));
            spotlight.SetPropertyBlock(mpb);
            mpb = new();
            ringLight.GetPropertyBlock(mpb);
            Vector4 rColor = Vector4.Lerp(currRColor, color, lerpVal);
            mpb.SetVector("_Color", rColor * Mathf.Pow(2f, rIntensity));
            ringLight.SetPropertyBlock(mpb);
            Vector4 lColor = Vector4.Lerp(currLightColor, color, lerpVal);
            lightComponent.color = lColor;
            yield return null;
        }

    }
}
