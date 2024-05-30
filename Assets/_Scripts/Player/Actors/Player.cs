using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    #region Data Attributes
    [SerializeField] private PlayerData _data;
    public PlayerData Data => Data;

    private int _inventorySlots;
    private float _throwCooldown;
    private float _throwForce;
    private float _throwDistance;

    public int InventorySlots => _inventorySlots;
    public float ThrowCooldown => _throwCooldown;
    public float ThrowForce => _throwForce;
    public float ThrowDistance => _throwDistance;
    #endregion

    [SerializeField] public List<GameObject> currOrbs;  // testing
    //[SerializeField] public List<GameObject> currOrbs { get; protected set; }

    void Start() {
        InitializeAttributes();
    }

    protected void InitializeAttributes() {
        _inventorySlots = _data.InventorySlots;
        _throwCooldown = _data.ThrowCooldown;
        _throwForce = _data.ThrowForce;
        _throwDistance = _data.ThrowDistance;
    }
}
