using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSwitch : MonoBehaviour {

    [SerializeField] private OrbThrow[] orbs;
    [SerializeField] private OrbAlter[] alters;
    [SerializeField] private Animator planeAnim;
    [SerializeField] private Animator switchAnim;

    void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Player player)) {
            foreach (OrbThrow orb in orbs) {
                orb.gameObject.SetActive(true);
                orb.ForceReturn(player);
            }
            foreach (OrbAlter alter in alters) {
                alter.ReleaseOrb(player);
            }
            switchAnim.SetTrigger("Press");
            planeAnim.SetTrigger("PushBlink");
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.TryGetComponent(out Player _)) {
            switchAnim.SetTrigger("Unpress");
        }
    }
}