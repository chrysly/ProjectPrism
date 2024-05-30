using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class that handles player movement
/// </summary>
public class PlayerController : MonoBehaviour 
{
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private int _camAngleSkew = 45;    // camera isometric skew in degrees
    private Rigidbody _rb;

    private PlayerActionMap _playerActionMap;
    private Vector3 _moveVector;

    void Awake() {
        _rb = GetComponent<Rigidbody>();
        // get player inputs
        _playerActionMap = new PlayerActionMap();
        _playerActionMap.Player.Enable();
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
}
