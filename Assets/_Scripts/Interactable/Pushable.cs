using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For Interactable objects that can be pushed
/// </summary>
public class Pushable : Interactable
{
    [SerializeField] private Animator planeLeft, planeRight,
                                      planeForward, planeBack;
    [SerializeField] protected float _speed = 1f;

    protected bool _isBeingPushed = false;

    protected Vector3 _destination;

    // will be the size of a standard tile
    protected float _detectionRadius = 1f;
    
    public override void InteractAction(OrbThrownData data) {
        base.InteractAction(data);
        PushObject();
    }

    void Update() {
        if (_isBeingPushed) {
            if (Vector3.Distance(_t.position, _destination) < Mathf.Epsilon) {
                _t.position = _destination;
                _isBeingPushed = false;
            } else {
                _t.position = Vector3.MoveTowards(_t.position, _destination, _speed * Time.deltaTime);
            }
        }
    }

    protected void PushObject() {
        if (!_isBeingPushed) {
            //_destination = _t.position + AlignToGrid(_hitData.PushDirection.normalized);
            _destination = _t.position + AlignToGrid(_hitData.PushDirection);
            _isBeingPushed = true;

            Debug.Log("destination: " + AlignToGrid(_hitData.PushDirection.normalized));
        }
    }

    protected Vector3 AlignToGrid(Vector3 input) {
        input.y = 0;

        // determine which direction value is greater
        //if (Mathf.Abs(input.x) >= Mathf.Abs(input.z)) { input.z = 0; }
        //else { input.x = 0; }

        return input;
    }

    /// <summary>
    /// If something is blocking the objects path then don't move block
    /// </summary>
    protected bool CheckDirection() {
        if (Physics.Raycast(_t.position, _hitData.PushDirection, out _, _detectionRadius)) { return false; }
        return true;
    }
}
