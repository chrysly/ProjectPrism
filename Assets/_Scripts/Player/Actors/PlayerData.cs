using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actor Data/Player")]
public class PlayerData : ActorData 
{
    [SerializeField] private int _inventorySlots = 1;

    [Header("Throw Variables")]
    [SerializeField] private float _throwCooldown = 0;
    [SerializeField] private float _throwForce = 0;
    [SerializeField] private float _throwDistance = 0;

    [Header("Movement Variables")]
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _turnSpeed = 20f;
    [SerializeField] private float _linearDrag = 20f;
    [SerializeField] private float _moveAccel = 0.2f;
    [SerializeField] private float _gravityVal;

    // Accessors ---
    public int InventorySlots => _inventorySlots;
    public float ThrowCooldown => _throwCooldown;
    public float ThrowForce => _throwForce;
    public float ThrowDistance => _throwDistance;

    public float MoveSpeed => _moveSpeed;
    public float TurnSpeed => _turnSpeed;
    public float MoveAccel => _moveAccel;
    public float LinearDrag => _linearDrag;
    public float GravityVal => _gravityVal;
}
