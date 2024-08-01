using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAAAAAAAAAA : MonoBehaviour
{
    [SerializeField] GameObject anim;
    [SerializeField] private float waitTime;

    private void Awake() {
        anim.SetActive(false);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn() {
        yield return new WaitForSeconds(waitTime);
        anim.SetActive(true);
    }
}
