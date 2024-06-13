using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Togglable : MonoBehaviour {
    [SerializeField] protected OrbThrownData.OrbColor colorMatch;

    public virtual void Toggle(OrbThrownData data) {
        if (data.Color == colorMatch) {
            Enable(data);
        }
        else {
            Disable(data);
        }
    } 
    
    protected virtual void Enable(OrbThrownData data) {
        this.gameObject.SetActive(true);
    }

    protected virtual void Disable(OrbThrownData data) {
        this.gameObject.SetActive(false);
    }
}
