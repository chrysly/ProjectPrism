using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RespawnDriver : MonoBehaviour {
    private ColorDriver _colorDriver;
    private MeshRenderer _renderer;
    private Vector3 _respawnPoint;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDuration = 1f;

    private void Start() {
        _renderer = GetComponentInChildren<MeshRenderer>();
        _colorDriver = GetComponentInChildren<ColorDriver>();
        _respawnPoint = respawnPoint.position;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            RespawnCheck(EColor.Cyan);
        }
    }

    public void RespawnCheck(EColor colorPass) {
        if (colorPass != _colorDriver.GetColor()) {
            StartCoroutine(RespawnAction());
        }
    }

    private IEnumerator RespawnAction() {
        MaterialPropertyBlock mpb = new ();
        _renderer.GetPropertyBlock(mpb);
        DOVirtual.Float(1f, 0f, respawnDuration / 2f, (float value) => {
            mpb.SetFloat("_Alpha", value);
            _renderer.SetPropertyBlock(mpb);
        });
        CharacterController controller = GetComponentInChildren<CharacterController>();
        controller.enabled = false;
        yield return new WaitForSeconds(0.5f);
        transform.position = _respawnPoint;
        controller.enabled = true;
        DOVirtual.Float(0f, 1f, respawnDuration / 2f, (float value) => {
            mpb.SetFloat("_Alpha", value);
            _renderer.SetPropertyBlock(mpb);
        });
    }
    
}
