using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Refactor to "Receiver" class
public class Togglable : MonoBehaviour {
    [SerializeField] protected OrbThrownData.OrbColor colorMatch;

    //Other scripts must call this method to check if the correct color is triggered
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
