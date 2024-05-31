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
    #endregion

    [SerializeField] private List<GameObject> heldOrbs;
    [SerializeField] private List<GameObject> thrownOrbs;

    [SerializeField] private List<GameObject> startingOrbs;
    //[SerializeField] public List<GameObject> currOrbs { get; protected set; }

    void Start() {
        InitializeAttributes();
        InitialSpawnOrbs();

        foreach (GameObject obj in startingOrbs) {

        }
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
        //foreach (GameObject obj in heldOrbs) {
        //    GameObject instantiatedChild = Instantiate(obj);
        //    heldOrbs.Add(instantiatedChild);
        //    instantiatedChild.SetActive(false);
        //}
    }

    // functions to move orb arounds in different states
    public void AddHeldOrb(GameObject orb) {
        heldOrbs.Add(orb);
    }

    //public GameObject RemoveHeldOrb(GameObject orb) {
    //    GameObject returnObj = 
    //}

    public void AddThrownOrb(GameObject orb) {
        heldOrbs.Add(orb);
    }

    //public GameObject RemoveThrownOrb(GameObject orb) {

    //}
}
