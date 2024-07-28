using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class that handles player movement
/// </summary>
public class PlayerController : MonoBehaviour 
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Player _player;
    [SerializeField] private OrbHandler _orbHandler;
    [SerializeField] private Animator animator;
    private bool _canThrow = true;

    private float _moveSpeed;
    private float _alignmentMult = 1f;

    private Vector3 _floorMove;
    private Vector3 _floorDir;
    private Vector3 _verticalMove;

    /// <summary> Transform of this object </summary>
    private Transform _t;

    void Start() {
        _t = transform;
        _floorDir = _t.forward;

        // get player inputs
        GameManager.Instance.EnterPlayerControls();
        OrbHandler.OnThrow += PlayerThrow;
    }

    void Update() {
        FloorMovement();
        FloorRotation();
        VerticalMovement();
        _controller.Move((_floorMove + _verticalMove) * Time.deltaTime);
    }

    private void FloorMovement() {
        Vector2 inputVector = GameManager.Instance.PlayerActionMap.Player.Movement.ReadValue<Vector2>();
        bool isMoving = inputVector.magnitude != 0;

        if (isMoving) {
            _moveSpeed = Mathf.MoveTowards(_moveSpeed, _player.MoveSpeed, _player.MoveAccel * Time.deltaTime);
            Vector3 camRight = Camera.main.transform.right,
                    camForward = Camera.main.transform.forward;
            camRight.y = 0;
            camForward.y = 0;
            camRight.Normalize();
            camForward.Normalize();

            _floorDir = (inputVector.x * camRight + inputVector.y * camForward).normalized;
        } else {
            _moveSpeed = Mathf.MoveTowards(_moveSpeed, 0, _player.LinearDrag * Time.deltaTime);
        }

        _floorMove = _floorDir * _moveSpeed * _alignmentMult;
        animator.SetFloat("MoveSpeed", (_moveSpeed * Mathf.Max(0.5f, _alignmentMult)) / _player.MoveSpeed);
    }

    private void FloorRotation() {
        if (_controller.isGrounded) {
            Quaternion targetRotation = Quaternion.LookRotation(_floorDir, Vector3.up);
            _t.rotation = Quaternion.RotateTowards(_t.rotation, targetRotation, _player.TurnSpeed * Time.deltaTime);
            _alignmentMult = Mathf.Max(0, 1 - Quaternion.Angle(_t.rotation, targetRotation) / 180);
        }
    }

    private void VerticalMovement() {
        _verticalMove = _controller.isGrounded ? new Vector3(0, -1, 0)
                      : new Vector3 (0, _verticalMove.y - _player.GravityVal * Time.deltaTime, 0);
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