using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardZone : Zone {
    [SerializeField] private EColor safeColor;

    public EColor SafeColor() { return safeColor; }
    
    protected override void OnTriggerEnter(Collider other) {
        Debug.Log("Hazard zone entered");
        RespawnDriver driver = other.GetComponentInChildren<RespawnDriver>();
        if (driver != null) {
            driver.RespawnCheck(safeColor);
        }
    }

    protected override void OnDrawGizmos() {
        BoxCollider collider = GetComponent<BoxCollider>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + collider.center, collider.size);
    }
}
