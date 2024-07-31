using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCombine : MonoBehaviour
{
    [SerializeField] private Animator aniamtor;
    [SerializeField] private Transform[] lights;
    [SerializeField] private float randomAmplitude;
    [SerializeField] private float speedWeight;
    private Vector3[] anchors;

    void Start() {
        anchors = new Vector3[lights.Length];
        for (int i = 0; i < lights.Length; i++) {
            anchors[i] = lights[i].position;
        }
    }

    void Update() {
        if (Input.GetKey(KeyCode.H)) {
            aniamtor.SetTrigger("Play");
        }

        lights[0].position = anchors[0] + new Vector3(Mathf.Sin(Time.time * 4 * speedWeight), Mathf.Cos(Time.time * 6 * speedWeight), Mathf.Sin(Time.time * 7 * speedWeight)) * randomAmplitude;
        lights[1].position = anchors[1] + new Vector3(Mathf.Cos(Time.time * 4.8f * speedWeight), Mathf.Cos(Time.time * 5.2f * speedWeight), Mathf.Sin(Time.time * 4 * speedWeight)) * randomAmplitude;
        lights[2].position = anchors[2] + new Vector3(Mathf.Sin(Time.time * 4.2f * speedWeight), Mathf.Cos(Time.time * 3.8f * speedWeight), Mathf.Cos(Time.time * 5.8f * speedWeight)) * randomAmplitude;
    }
}
