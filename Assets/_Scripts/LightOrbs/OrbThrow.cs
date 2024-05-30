using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior of a thrown orb
/// </summary>
public class OrbThrow : MonoBehaviour {
    private GameObject _sender;    // what to return to
    private Player _player;
    private bool _thrown = false;
    private Vector3 _throwDirection;

    void Start() {
        _sender = GameObject.FindGameObjectWithTag("Player");
        _player = _sender.GetComponent<Player>();
        _throwDirection = new Vector3(_sender.transform.position.x, _sender.transform.position.y + 1, _sender.transform.position.z) + _sender.transform.forward * _player.ThrowDistance;
        Debug.Log(_throwDirection);
        StartCoroutine(Thrown());
    }

    void Update() {
        if (_thrown) {
            transform.position = Vector3.MoveTowards(transform.position, _throwDirection, _player.ThrowForce * Time.deltaTime);
        } else {
            // return to the player
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_sender.transform.position.x, _sender.transform.position.y + 1, _sender.transform.position.z), _player.ThrowForce * Time.deltaTime);
        }

        // once it gets close to the player destroy it
        if (!_thrown && Vector3.Distance(_sender.transform.position, transform.position) < 1.5) {
            _player.currOrbs.Add(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    IEnumerator Thrown() {
        _thrown = true;
        yield return new WaitForSeconds(1.5f);
        _thrown = false;
    }
}