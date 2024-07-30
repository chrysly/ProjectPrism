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

    [SerializeField] private TrailRenderer trail;
    
    public override void InteractAction(OrbThrownData data) {
        base.InteractAction(data);
        PushObject(data);
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

    protected void PushObject(OrbThrownData data) {
        if (!_isBeingPushed) {
            //_destination = _t.position + AlignToGrid(_hitData.PushDirection.normalized);

            Vector3 dir = _hitData.PushDirection.normalized;
            Animator planeAnim = ClosestCardinal(dir);

            SpriteRenderer[] renderers = planeAnim.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer renderer in renderers) renderer.color = data.Color.GetColor() * 1.9f;

            MaterialPropertyBlock mpb = new();
            trail.GetPropertyBlock(mpb);
            mpb.SetVector("_Color", data.Color.GetColor() * 1.9f);
            trail.SetPropertyBlock(mpb);

            planeAnim.SetTrigger("PushBlink");

            _destination = _t.position + AlignToGrid(_hitData.PushDirection);
            _isBeingPushed = true;

            //Debug.Log("destination: " + AlignToGrid(dir));
        }
    }

    private Animator ClosestCardinal(Vector3 dir) {
        (Vector3Int, Animator)[] options = new (Vector3Int, Animator)[] { (Vector3Int.right, planeRight),
                                                                          (Vector3Int.forward, planeForward),
                                                                          (Vector3Int.back, planeBack) };
        (Vector3Int, Animator) closestCardinal = (Vector3Int.left, planeLeft);
        foreach ((Vector3Int, Animator) kvp in options) {
            if (Vector3.Distance(dir, closestCardinal.Item1)
                < Vector3.Distance(dir, kvp.Item1)) closestCardinal = kvp;
        } 
        return closestCardinal.Item2;
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
