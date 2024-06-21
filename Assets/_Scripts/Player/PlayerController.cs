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
    [SerializeField] private Transform _throwPoint;

    private bool _canThrow = true;

    //private PlayerActionMap _playerActionMap;
    private Vector3 _moveVector;

    /// <summary>
    /// Transform of this object
    /// </summary>
    private Transform _t;

    void Start() {
        _t = this.transform;
        _controller = GetComponent<CharacterController>();

        // get player inputs
        GameManager.Instance.EnterPlayerControls();
        GameManager.Instance.PlayerActionMap.Player.Throw.performed += PlayerThrow;
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

        _controller.Move(_t.forward * _moveVector.magnitude * Time.deltaTime * _player.MoveSpeed);
    }
    
    private void PlayerThrow(InputAction.CallbackContext context) {
        if (_canThrow && _player.HeldOrbCount() > 0) {
            StartCoroutine(Throw());

            _player.GetOrb().SetActive(true);
            _player.GetOrb().GetComponent<OrbThrow>().ThrowOrb();

            // remove from curr orbs and add to thrown orbs list
            _player.AddThrownOrb(_player.RemoveHeldOrb(_player.GetOrb()));
        }
    }

    IEnumerator Throw() {
        _canThrow = false;
        yield return _player.ThrowCooldown;
        _canThrow = true;
    }
}
