using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    #region Data Attributes
    [SerializeField] private PlayerData _data;
    public PlayerData Data => Data;

    private int _inventorySlots;

    [Header("Throw Variables")]
    private float _throwCooldown;
    private float _throwForce;
    private float _throwDistance;
    [SerializeField] private Transform _throwPoint;

    [Header("Movement Variables")]
    private float _moveSpeed;
    private float _turnSpeed;
    private int _camAngleSkew;    // camera isometric skew in degrees

    // Accessors
    public int InventorySlots => _inventorySlots;
    public float ThrowCooldown => _throwCooldown;
    public float ThrowForce => _throwForce;
    public float ThrowDistance => _throwDistance;
    public float MoveSpeed => _moveSpeed;
    public float TurnSpeed => _turnSpeed;
    public int CameraAngleSkew => _camAngleSkew;
    public Transform ThrowPoint => _throwPoint;
    #endregion

    [SerializeField] private List<GameObject> heldOrbs;
    [SerializeField] private List<GameObject> thrownOrbs;

    public List<GameObject> startingOrbs; //test

    //[SerializeField] public List<GameObject> currOrbs { get; protected set; }

    void Start() {
        InitializeAttributes();
        InitialSpawnOrbs();
    }

    protected void InitializeAttributes() {
        _inventorySlots = _data.InventorySlots;
        _throwCooldown = _data.ThrowCooldown;
        _throwForce = _data.ThrowForce;
        _throwDistance = _data.ThrowDistance;
        _moveSpeed = _data.MoveSpeed;
        _turnSpeed = _data.TurnSpeed;
        _camAngleSkew = _data.CameraAngleSkew;
    }

    protected void InitialSpawnOrbs() {
        foreach (GameObject obj in startingOrbs) {
            GameObject instantiatedChild = Instantiate(obj);
            AddHeldOrb(instantiatedChild);
            instantiatedChild.SetActive(false);
        }
    }

    #region Custom List Functions
    public int HeldOrbCount() {
        return heldOrbs.Count;
    }

    public GameObject GetOrb() {
        return heldOrbs[0];
    }
    
    public void AddHeldOrb(GameObject orb) {
        heldOrbs.Add(orb);
    }

    public GameObject RemoveHeldOrb(GameObject orb) {
        heldOrbs.Remove(orb);
        return orb;
    }

    public void AddThrownOrb(GameObject orb) {
        thrownOrbs.Add(orb);
    }

    public GameObject RemoveThrownOrb(GameObject orb) {
        thrownOrbs.Remove(orb);
        return orb;
    }
    #endregion
}
