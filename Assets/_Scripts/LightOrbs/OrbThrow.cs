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
    private Transform _throwPoint;

    //void Awake() {
    //    Debug.Log("start for orb");
    //    _sender = GameObject.FindGameObjectWithTag("Player");
    //    _player = _sender.GetComponent<Player>();
    //    _t = this.transform;
    //    _senderTransform = _sender.transform;
    //    Debug.Log("END start for orb");
    //}

    void Update() {
        if (_thrown) {
            _t.position = Vector3.MoveTowards(_t.position, _throwDirection, _player.ThrowForce * Time.deltaTime);
        } else {
            // return to the player
            _t.position = Vector3.MoveTowards(_t.position, new Vector3(_throwPoint.position.x, _throwPoint.position.y, _throwPoint.position.z), _player.ThrowForce * Time.deltaTime);
        }

        // once it gets close to the player deactivate orb
        if (!_thrown && Vector3.Distance(_throwPoint.position, _t.position) < 1.5) {
            _player.AddHeldOrb(_player.RemoveThrownOrb(this.gameObject));
            this.gameObject.SetActive(false);
        }
    }

    public void ThrowOrb() {

        // well shit i have to initialize this again bc i deactivate the object
        _sender = GameObject.FindGameObjectWithTag("Player");
        _player = _sender.GetComponent<Player>();
        _t = this.transform;
        _throwPoint = _player.ThrowPoint;
        // ---

        this.transform.position = _throwPoint.position;
        _throwDirection = new Vector3(_throwPoint.position.x, _throwPoint.position.y, _throwPoint.position.z) + _throwPoint.forward * _player.ThrowDistance;
        StartCoroutine(Thrown());
    }

    IEnumerator Thrown() {
        _thrown = true;
        yield return new WaitForSeconds(1.5f);
        _thrown = false;
    }
}
