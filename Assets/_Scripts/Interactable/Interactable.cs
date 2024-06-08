using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for interactable objects
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string _objectName;
    [SerializeField] protected bool _canHoldOrb;
    protected GameObject _currentHeldOrb;

    protected GameObject _hitObject;

    public virtual void InteractAction(OrbThrownData data) {
        GetHitObject(data);
    }

    /// <summary>
    /// Gets the object this interactable was hit by
    /// </summary>
    protected void GetHitObject(OrbThrownData data) {
        _hitObject = data.OrbObject;

        // if this object can hold orbs, store that orb
        if (_canHoldOrb && data.OrbObject.GetComponent<OrbThrow>()) { HoldOrb(data.OrbObject); }
    }

    protected void HoldOrb(GameObject orb) {
        _currentHeldOrb = orb;
    }
}