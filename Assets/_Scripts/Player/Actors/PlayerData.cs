using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actor Data/Player")]
public class PlayerData : ActorData 
{
    [SerializeField] private int _inventorySlots = 1;
    [SerializeField] private float _throwCooldown;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _throwDistance;

    // Accessors
    public int InventorySlots => _inventorySlots;
    public float ThrowCooldown => _throwCooldown;
    public float ThrowForce => _throwForce;
    public float ThrowDistance => _throwDistance;
}
