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
    [SerializeField] protected int _maxHeldOrbs;
    protected List<GameObject> _currentHeldOrbs = new List<GameObject>();

    protected OrbThrownData _hitData;

    /// <summary>
    /// Transform of this
    /// </summary>
    protected Transform _t;

    void Start() {
        _t = this.transform;
    }

    public virtual void InteractAction(OrbThrownData data) {
        _hitData = data;
        HoldOrb();
    }

    protected void HoldOrb() {
        if (_canHoldOrb) {
            if (_currentHeldOrbs.Count < _maxHeldOrbs) {
                _currentHeldOrbs.Add(_hitData.OrbObject);
                _hitData.OrbObject.GetComponent<OrbThrow>().OrbOff();
            } else {
                // swap orbs
                _currentHeldOrbs[0].GetComponent<OrbThrow>().OrbOn();
                _currentHeldOrbs.Remove(_currentHeldOrbs[0]);
                _currentHeldOrbs.Add(_hitData.OrbObject);
                _hitData.OrbObject.GetComponent<OrbThrow>().OrbOff();
            }
        }
    }
}