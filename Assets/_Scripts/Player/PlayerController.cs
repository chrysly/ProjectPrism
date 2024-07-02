using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class that handles player movement
/// </summary>
public class PlayerController : MonoBehaviour 
{
    private CharacterController _controller;
    [SerializeField] private Player _player;

    private OrbHandler _orbHandler;
    private bool _canThrow = true;

    //private PlayerActionMap _playerActionMap;
    private Vector3 _moveVector;
    private float verticalVelocity;

    /// <summary>
    /// Transform of this object
    /// </summary>
    private Transform _t;

    void Start() {
        _t = this.transform;
        _controller = GetComponent<CharacterController>();
        _orbHandler = this.gameObject.GetComponent<OrbHandler>();

        // get player inputs
        GameManager.Instance.EnterPlayerControls();
        OrbHandler.OnThrow += PlayerThrow;
    }

    void FixedUpdate() {
        PlayerMove();
        PlayerLook();
    }

    // rotates the player within isometric
    private void PlayerLook() {
        if (_moveVector != Vector3.zero) {
            //fixes isometric jank
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, _player.CameraAngleSkew, 0));
            var skewedInput = matrix.MultiplyPoint3x4(_moveVector);

            var relative = (_t.position + skewedInput) - _t.position; // angle between where we're moving
            var rot = Quaternion.LookRotation(relative, Vector3.up);    // axis which we rotate around

            //_t.rotation = Quaternion.RotateTowards(_t.rotation, rot, _player.TurnSpeed * Time.deltaTime);  // if want lerp
            transform.rotation = rot;
        }
    }

    private void PlayerMove() {
        Vector2 inputVector = GameManager.Instance.PlayerActionMap.Player.Movement.ReadValue<Vector2>();
        _moveVector = new Vector3(inputVector.x, 0, inputVector.y);

        //if (!_controller.isGrounded) {
        //    verticalVelocity -= _player.GravityVal * Time.deltaTime;
        //    _moveVector.y = verticalVelocity;
        //    _controller.Move(_t.up * _moveVector.magnitude * Time.deltaTime * _player.MoveSpeed);
        //} else {
            _controller.Move(_t.forward * _moveVector.magnitude * Time.deltaTime * _player.MoveSpeed);
        //}
    }
    
    private void PlayerThrow(GameObject orb) {
        if (_canThrow) {
            StartCoroutine(Throw());

            orb.SetActive(true);
            orb.GetComponent<OrbThrow>().ThrowOrb();
        }
    }

    IEnumerator Throw() {
        _canThrow = false;
        yield return _player.ThrowCooldown;
        _canThrow = true;
    }
}
