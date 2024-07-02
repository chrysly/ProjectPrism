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
        GameManager.Instance.PlayerActionMap.Player.SwapOrbs.performed += SwapOrbs;

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

    public GameObject RemoveOrb() {
        if (_heldOrbsCount > 0) {
            GameObject toReturn = _heldOrbs[0];
            _heldOrbs[0] = null;
            _heldOrbsCount--;
            for (int i = 0; i < _heldOrbsCount; i++) {
                _heldOrbs[i] = _heldOrbs[i + 1];
            }
            _heldOrbs[_heldOrbsCount] = null;

            return toReturn;
        }
        return null;
    }

    private void SwapOrbs(InputAction.CallbackContext context) {
        if (_heldOrbsCount <= 1) { return; }    // not enough orbs to swap

        if (context.ReadValue<float>() >= 0) {    // swap to the right
            GameObject _removedOrb = RemoveOrb();
            _heldOrbs[_heldOrbsCount] = _removedOrb;
            _heldOrbsCount++;
        } else {    // swap to the left
            GameObject _removedOrb = RemoveOrb();       // jank method
            _heldOrbs[_heldOrbsCount] = _removedOrb;
            _heldOrbsCount++;

            _removedOrb = RemoveOrb();
            _heldOrbs[_heldOrbsCount] = _removedOrb;
            _heldOrbsCount++;
        }
    }
}
