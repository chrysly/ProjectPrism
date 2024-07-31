using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

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
    //Added for identification of orbs
    [SerializeField] private EColor _color;
    private float _animStartTime = 0f;

    // Accessors
    public EColor Color => _color;
    
    // apparently this is like 2 times faster bc transform is an extern
    /// <summary>
    /// Transform of this object
    /// </summary>
    private Transform _t;
    private Transform _throwPoint;

    /// DREAMHACK DATA
    public VisualEffect shineFX;
    public VisualEffect sparkFX;
    private bool interrupt;

    void OnEnable() {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    void FixedUpdate() {
        if (!interrupt) MoveOrb();
    }

    public void OrbOff() {
        //TODO: refactor after trailer, this should not be here lol
        GetComponentInChildren<TrailRenderer>().Clear();
        StopAllCoroutines();
        StartCoroutine(Menguate());
    }

    private IEnumerator Menguate() {
        GetComponent<Rigidbody>().isKinematic = true;
        shineFX.Stop();
        sparkFX.Stop();
        while (Vector3.Distance(_t.localScale, Vector3.zero) > Mathf.Epsilon) {
            _t.localScale = Vector3.MoveTowards(_t.localScale, Vector3.zero, Time.deltaTime);
            yield return null;
        } yield return new WaitUntil(() => sparkFX.aliveParticleCount == 0);
        gameObject.SetActive(false);
    }

    public void OrbOn() {
        this.gameObject.SetActive(true);    // does this work???
    }

    // want to refactor how vars are taken from player SO and used here as variables
    #region Throwing Orbs
    private void MoveOrb() {
        if (_thrown) {  
            // move towards desired point
            _t.position = Vector3.MoveTowards(_t.position, _throwPoint.position + _throwDirection.normalized * _player.ThrowDistance, 
                                             _player.ThrowForce * Time.deltaTime * _speedCurve.Evaluate(TimeManagement()));
        } else { 
            // return to the player
            _t.position = Vector3.MoveTowards(_t.position, _throwPoint.position, _player.ThrowForce * Time.deltaTime);
        }

        // once it gets close to the player deactivate orb
        if (!_thrown && Vector3.Distance(_throwPoint.position, _t.position) < _collectRadius) {
            _animStartTime = 0;

            _player.OrbHandler.AddOrb(gameObject);
            OrbOff();

            //_player.AddHeldOrb(_player.RemoveThrownOrb(this.gameObject));
            //OrbOff();
        }
    }

    /* private void OnDrawGizmos() {
        if (Event.current.type == EventType.Repaint) {
            UnityEditor.Handles.ArrowHandleCap(0, _throwPoint.position, Quaternion.LookRotation(_throwDirection, Vector3.up), 2, EventType.Repaint);
        }
    }*/

    public void ThrowOrb() {
        // well shit i have to initialize this again bc i deactivate the object --> looking for solutions....
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Grow());
        _sender = GameObject.FindGameObjectWithTag("Player");
        _player = _sender.GetComponent<Player>();
        _t = this.transform;
        _throwPoint = _player.ThrowPoint;
        // ---

        _t.position = _throwPoint.position;
        _throwDirection = _throwPoint.forward * _player.ThrowDistance;
        StartCoroutine(Thrown());
    }

    private IEnumerator Grow() {
        shineFX.Play();
        sparkFX.Play();
        transform.localScale = Vector3.zero;
        while (transform.localScale != Vector3.one * 0.3f) {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * 0.3f, Time.deltaTime);
            yield return null;
        }
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

    public void ForceReturn(Player player) {
        _t = transform;
        _player = player;
        _throwPoint = player.ThrowPoint;
        _thrown = false;
    }

    /// <summary>
    /// For when the orb comes into contact with an interactable
    /// </summary>
    void OnTriggerEnter(Collider coll) {
        if (!_thrown) { return; }

        // probably send out an event for animation?
        _thrown = false;
        
        /// NPC ABSORB PLACEHOLDER. REMOVE AFTER DREAMHACK!
        if (coll.TryGetComponent(out NPC _)) {
            StartCoroutine(NPCAbsorb(coll.transform));
            interrupt = true;
        } else if (coll.TryGetComponent(out OrbAlter alter)) {
            StartCoroutine(PillarOrbit(alter, new OrbThrownData(this.gameObject, _throwDirection, _color)));
            interrupt = true;
            return;
        }

        if (!coll.GetComponent<Player>()) SpawnFX(transform.position);

        Interactable interactable = coll.GetComponent<Interactable>();

        if (interactable != null) {
            interactable.InteractAction(new OrbThrownData(this.gameObject, _throwDirection, _color));
        }
    }

    /// <summary>
    /// NPC ABSORB PLACEHOLDER. REMOVE AFTER DREAMHACK!
    /// </summary>
    private IEnumerator NPCAbsorb(Transform nTran) {
        if (TryGetComponent(out Rigidbody rb)) Destroy(rb);
        while (transform.localScale != Vector3.zero) {
            transform.position = Vector3.MoveTowards(transform.position, nTran.position, Time.deltaTime * 2);
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime);
            yield return null;
        }
    }

    /// <summary>
    /// PLACEHOLDER, REMOVE AFTER DREAMHACK!
    /// </summary>
    /// <returns></returns>
    private IEnumerator PillarOrbit(OrbAlter alter, OrbThrownData data) {
        if (TryGetComponent(out Rigidbody rb)) { Debug.Log("hey"); Destroy(rb); }
        while (_t.position != alter.path.position) {
            _t.position = Vector3.MoveTowards(_t.position, alter.path.position, _player.ThrowForce * Time.deltaTime);
            yield return null;
        } alter.pathAnimator.SetTrigger("MovePath");
        yield return null;
        while (!alter.pathAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            _t.position = alter.path.position;
            yield return null;
        } alter.InteractAction(data);
    }


    public EColor GetOrbColor() {
        return _color;
    }
    
    //TODO: DELETE, TEMP FOR VISUALS
    #region Temp

    [SerializeField] private GameObject orbFX;

    private void SpawnFX(Vector3 pos) {
        GameObject fx = Instantiate(orbFX, pos, Quaternion.identity);
        Destroy(fx, 2f);
    }

    #endregion Temp
}
