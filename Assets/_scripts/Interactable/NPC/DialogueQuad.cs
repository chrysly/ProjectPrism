using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DialogueQuad : MonoBehaviour
{
    void Start() {
        
    }
    #if UNITY_EDITOR
    void OnDrawGizmos() {
        //UnityEditor.SceneView.lastActiveSceneView.sceneViewState.alwaysRefresh = true;
        transform.LookAt(transform.position - (Camera.main.transform.position - transform.position));
    }
    #endif
}
