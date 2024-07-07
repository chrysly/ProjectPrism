using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializing the player attributes
/// </summary>
public class Player : Actor
{
    #region Data Attributes
    [SerializeField] private PlayerData _data;
    public PlayerData Data => Data;

    [Header("Movement Variables")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private int _camAngleSkew;    // camera isometric skew in degrees
    [SerializeField] private float _gravityVal;

    [Header("Throw Variables")]
    [SerializeField] private int _inventorySlots;
    [SerializeField] private float _throwCooldown;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _throwDistance;
    [SerializeField] private Transform _throwPoint;

    [Header("Other")]
    public List<GameObject> startingOrbs; //test

    private OrbHandler _orbHandler;

    // Accessors
    public int InventorySlots => _inventorySlots;
    public float ThrowCooldown => _throwCooldown;
    public float ThrowForce => _throwForce;
    public float ThrowDistance => _throwDistance;
    public float MoveSpeed => _moveSpeed;
    public float TurnSpeed => _turnSpeed;
    public int CameraAngleSkew => _camAngleSkew;
    public Transform ThrowPoint => _throwPoint;
    public OrbHandler OrbHandler => _orbHandler;
    public float GravityVal => _gravityVal;
    #endregion

    void Awake() {
        InitializeAttributes();
    }

    void Start() {
        _orbHandler = this.gameObject.GetComponent<OrbHandler>();
    }

    protected void InitializeAttributes() {
        _inventorySlots = _data.InventorySlots;
        _throwCooldown = _data.ThrowCooldown;
        _throwForce = _data.ThrowForce;
        _throwDistance = _data.ThrowDistance;
        _moveSpeed = _data.MoveSpeed;
        _turnSpeed = _data.TurnSpeed;
        _camAngleSkew = _data.CameraAngleSkew;
        _gravityVal = _data.GravityVal;
    }
}
