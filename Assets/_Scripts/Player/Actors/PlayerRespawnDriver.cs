using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathDriver : MonoBehaviour {
    [SerializeField] private Transform respawnPoint;

    private ColorDriver _driver;
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(respawnPoint.position, Vector3.one * 1f);
    }

    private void RespawnPlayer() {
        GameManager.Instance.ExitPlayerControls();
    }
}
