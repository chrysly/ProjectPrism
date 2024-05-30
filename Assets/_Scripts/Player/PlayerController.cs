using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class that handles player movement
/// </summary>
public class PlayerController : MonoBehaviour 
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private int _camAngleSkew = 45;    // camera isometric skew in degrees
    private Rigidbody _rb;

    private bool _canThrow = true;

    private PlayerData _playerData;
    private PlayerActionMap _playerActionMap;
    private Vector3 _moveVector;

    void Awake() {
        _rb = GetComponent<Rigidbody>();
        // get player inputs
        _playerActionMap = new PlayerActionMap();
        _playerActionMap.Player.Enable();
        _playerActionMap.Player.Throw.performed += PlayerThrow;

        // change this later prolly
        _playerData = (PlayerData)_player.Data;
    }

    void FixedUpdate() {
        PlayerMove();
        PlayerLook();
    }

    // rotates the player within isometric
    private void PlayerLook() {
        if (_moveVector != Vector3.zero) {
            //fixes isometric jank
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, _camAngleSkew, 0));
            var skewedInput = matrix.MultiplyPoint3x4(_moveVector);

            var relative = (transform.position + skewedInput) - transform.position; // angle between where we're moving
            var rot = Quaternion.LookRotation(relative, Vector3.up);    // axis which we rotate around

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);  // if want lerp
            //transform.rotation = rot;
        }
    }

    private void PlayerMove() {
        Vector2 inputVector = _playerActionMap.Player.Movement.ReadValue<Vector2>();
        Debug.Log(inputVector);

        _moveVector = new Vector3(inputVector.x, 0, inputVector.y);

        _rb.MovePosition(transform.position + (transform.forward * _moveVector.magnitude) * _speed * Time.deltaTime);
    }
    
    private void PlayerThrow(InputAction.CallbackContext context) {
        if (_canThrow) {
            StartCoroutine(Throw());
            if (_player.currOrbs.Count > 0) {
                Instantiate(_player.currOrbs[0], _throwPoint);
                _player.currOrbs.RemoveAt(0);
            }
        }
    }

    IEnumerator Throw() {
        _canThrow = false;
        yield return _playerData.ThrowCooldown;
        _canThrow = true;
    }
}
