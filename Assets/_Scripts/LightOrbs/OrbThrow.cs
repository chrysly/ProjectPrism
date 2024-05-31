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

    // apparently this is like 2 times faster bc transform is an extern
    private Transform _t;   // transform of this
    private Transform _senderTransform;

    void Start() {
        Debug.Log("start for orb");
        _sender = GameObject.FindGameObjectWithTag("Player");
        _player = _sender.GetComponent<Player>();
        _t = this.transform;
        _senderTransform = _sender.transform;
        Debug.Log("END start for orb");
    }

    void Update() {
        if (_thrown) {
            _t.position = Vector3.MoveTowards(_t.position, _throwDirection, _player.ThrowForce * Time.deltaTime);
        } else {
            // return to the player
            _t.position = Vector3.MoveTowards(_t.position, new Vector3(_senderTransform.position.x, _senderTransform.position.y + 1, _senderTransform.position.z), _player.ThrowForce * Time.deltaTime);
        }

        // once it gets close to the player deactivate orb
        if (!_thrown && Vector3.Distance(_senderTransform.position, _t.position) < 1.5) {
            //_player.heldOrbs.Add(this.gameObject); // is this bad idk
        }
    }

    public void ThrowOrb() {
        _throwDirection = new Vector3(_senderTransform.position.x, _senderTransform.position.y + 1, _senderTransform.position.z) + _senderTransform.forward * _player.ThrowDistance;
        StartCoroutine(Thrown());
    }

    IEnumerator Thrown() {
        _thrown = true;
        yield return new WaitForSeconds(1.5f);
        _thrown = false;
    }
}
