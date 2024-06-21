using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton for persistant Data
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    // input action maps
    private PlayerActionMap _playerActionMap;
    public PlayerActionMap PlayerActionMap => _playerActionMap;

    private void Awake() {
        if (_instance != null) { Destroy(gameObject); }
        else {
            _instance = this;
        }

        _playerActionMap = new PlayerActionMap();
    }

    public void EnterPlayerControls() {
        _playerActionMap.Player.Enable();
        _playerActionMap.UIControl.Disable();
    }

    public void EnterUIControls() {
        _playerActionMap.UIControl.Enable();
        _playerActionMap.Player.Disable();
    }
}
