using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior of a thrown orb
/// </summary>
public class OrbThrow : MonoBehaviour {
    private GameObject _sender;    // what to return to
    private Player _player;
    private bool _thrown = false;
    private Vector3 _throwDirection;

    [SerializeField] private float _throwTime;
    [SerializeField] private float _collectRadius;
    [SerializeField] private AnimationCurve _speedCurve;
    private float _animStartTime = 0f;

    // apparently this is like 2 times faster bc transform is an extern
    /// <summary>
    /// Transform of this object
    /// </summary>
    private Transform _t;
    private Transform _throwPoint;

    void FixedUpdate() {
        MoveOrb();
    }

    public void OrbOff() {
        this.gameObject.SetActive(false);
    }

    public void OrbOn() {
        this.gameObject.SetActive(true);    // does this work???
    }
    
    // want to refactor how vars are taken from player SO and used here as variables
    #region Throwing Orbs
    private void MoveOrb() {
        if (_thrown) {  
            // move towards desired point
            _t.position = Vector3.MoveTowards(_t.position, _throwDirection, _player.ThrowForce * Time.deltaTime * _speedCurve.Evaluate(TimeManagement()));
        } else { 
            // return to the player
            _t.position = Vector3.MoveTowards(_t.position, new Vector3(_throwPoint.position.x, _throwPoint.position.y, _throwPoint.position.z), _player.ThrowForce * Time.deltaTime);
        }

        // once it gets close to the player deactivate orb
        if (!_thrown && Vector3.Distance(_throwPoint.position, _t.position) < _collectRadius) {
            _animStartTime = 0;
            _player.AddHeldOrb(_player.RemoveThrownOrb(this.gameObject));
            OrbOff();
        }
    }

    public void ThrowOrb() {

        // well shit i have to initialize this again bc i deactivate the object --> looking for solutions....
        _sender = GameObject.FindGameObjectWithTag("Player");
        _player = _sender.GetComponent<Player>();
        _t = this.transform;
        _throwPoint = _player.ThrowPoint;
        // ---

        this.transform.position = _throwPoint.position;
        _throwDirection = new Vector3(_throwPoint.position.x, _throwPoint.position.y, _throwPoint.position.z) + _throwPoint.forward * _player.ThrowDistance;
        StartCoroutine(Thrown());

        Debug.Log("Throw direction: " + _throwDirection);
    }

    IEnumerator Thrown() {
        _thrown = true;
        yield return new WaitForSeconds(_throwTime);
        _thrown = false;
    }

    /// <summary>
    /// Get time percent to plug into animation curve
    /// </summary>
    private float TimeManagement() {
        _animStartTime += Time.deltaTime;
        return _animStartTime / _throwTime;
    }
    #endregion

    /// <summary>
    /// For when the orb comes into contact with an interactable
    /// </summary>
    void OnTriggerEnter(Collider coll) {
        if (!_thrown) { return; }

        // probably send out an event for animation?
        _thrown = false;

        Interactable interactable = coll.GetComponent<Interactable>();

        if (interactable != null) {
            interactable.InteractAction(new OrbThrownData(this.gameObject, _throwDirection));
        }
    }
}
