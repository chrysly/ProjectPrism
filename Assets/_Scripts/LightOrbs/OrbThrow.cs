using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior of a thrown orb
/// </summary>
public class OrbThrow : MonoBehaviour
{
    [SerializeField] private GameObject _sender;    // what to return to
    private Player _player;
    private PlayerData _playerData;
    private bool _thrown = false;
    private Vector3 _throwDirection;

    void Start() {
        _player = _sender.GetComponent<Player>();
        _playerData = (PlayerData)_player.Data;
        _throwDirection = new Vector3(_sender.transform.position.x, _sender.transform.position.y + 1, _sender.transform.position.z) + _sender.transform.forward * _playerData.ThrowDistance;
        StartCoroutine(Thrown());
    }

    void Update() {
        if (_thrown) {
            transform.position = Vector3.MoveTowards(transform.position, _throwDirection, _playerData.ThrowForce * Time.deltaTime);
        } else {
            // return to the player
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_sender.transform.position.x, _sender.transform.position.y + 1, _sender.transform.position.z), Time.deltaTime * 40);
        }

        // once it gets close to the player destroy it
        if (!_thrown && Vector3.Distance(_sender.transform.position, transform.position) < 1.5) {
            _player.currOrbs.Add(this.gameObject);
            Destroy(this);
        }
    }

    IEnumerator Thrown() {
        _thrown = true;
        yield return new WaitForSeconds(1.5f);
        _thrown = false;
    }
}
