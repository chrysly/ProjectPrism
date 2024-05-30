using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actor Data/Player")]
public class PlayerData : ActorData 
{
    [SerializeField] private int _inventorySlots = 1;
}
