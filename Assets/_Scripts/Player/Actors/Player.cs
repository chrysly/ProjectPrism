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

    [Header("Throw Variables")]
    private int _inventorySlots;
    private float _throwCooldown;
    private float _throwForce;
    private float _throwDistance;
    [SerializeField] private Transform _throwPoint;

    [Header("Movement Variables")]
    private float _moveSpeed;
    private float _turnSpeed;
    private int _camAngleSkew;    // camera isometric skew in degrees
    private float _gravityVal;

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
