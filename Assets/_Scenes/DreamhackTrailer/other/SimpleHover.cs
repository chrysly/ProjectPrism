using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHover : MonoBehaviour
{
    [SerializeField] private float amp;
    [SerializeField] private float freq;
    private Vector3 _initialPos;

    private Transform _t;

    void Start() {
        _t = this.transform;
        _initialPos = new Vector3(_t.position.x, _t.position.y, _t.position.z);
    }
    
    void Update() {
        transform.position = new Vector3(_t.position.x, _t.position.y + Mathf.Sin(Time.time * freq) * amp, _t.position.z);
    }
}
