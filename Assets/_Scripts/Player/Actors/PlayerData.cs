using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actor Data/Player")]
public class PlayerData : ActorData 
{
    [SerializeField] private int _inventorySlots = 1;
    [SerializeField] private float _throwCooldown = 0;
    [SerializeField] private float _throwForce = 0;
    [SerializeField] private float _throwDistance = 0;

    // Accessors
    public int InventorySlots => _inventorySlots;
    public float ThrowCooldown => _throwCooldown;
    public float ThrowForce => _throwForce;
    public float ThrowDistance => _throwDistance;
}
