using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles the player orb inventory
/// </summary>
public class OrbHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _heldOrbs;
    private int _heldOrbsCount = 0;

    /// <summary>
    /// Event for when the player throws an orb
    /// </summary>
    public delegate void Throw(GameObject orb);
    public static event Throw OnThrow;

    private Player _player;

    // initialize the inventory
    private void Start() {
        _player = GetComponent<Player>();
        _heldOrbs = new GameObject[_player.InventorySlots];
        GameManager.Instance.PlayerActionMap.Player.Throw.performed += ThrowOrb;

        // initialize spawn orbs
        foreach (GameObject obj in _player.startingOrbs) {
            GameObject instantiatedChild = Instantiate(obj);
            AddOrb(instantiatedChild);
        }
    }

    /// <summary>
    /// Throw the first orb in the inventory slot
    /// </summary>
    private void ThrowOrb(InputAction.CallbackContext context) {
        if (_heldOrbsCount > 0) {
            OnThrow(_heldOrbs[0]);
            RemoveOrb();
        }
    }

    public bool AddOrb(GameObject orb) {
        if (_heldOrbsCount < _heldOrbs.Length) {
            orb.SetActive(false);
            _heldOrbs[_heldOrbsCount] = orb;
            _heldOrbsCount++;
            return true;
        }
        return false;
    }

    public bool RemoveOrb() {
        if (_heldOrbsCount > 0) {
            _heldOrbs[0] = null;
            _heldOrbsCount--;
            for (int i = 0; i < _heldOrbsCount; i++) {
                _heldOrbs[i] = _heldOrbs[i + 1];
            }
            _heldOrbs[_heldOrbsCount] = null;

            return true;
        }
        return false;
    }
}
