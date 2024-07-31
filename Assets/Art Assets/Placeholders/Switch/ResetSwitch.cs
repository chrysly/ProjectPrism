using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSwitch : MonoBehaviour {

    [SerializeField] private OrbThrow[] orbs;
    [SerializeField] private Animator planeAnim;

    void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Player player)) {
            foreach (OrbThrow orb in orbs) {
                orb.gameObject.SetActive(true);
                orb.ForceReturn(player);
            }
            //planeAnim.SetTrigger("");
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.TryGetComponent(out Player plater)) {
            //planeAnim.SetTrigger("");
        }
    }
}
