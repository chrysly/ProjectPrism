using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor {

    bool showDefaultInspector;

    private bool h_References, h_Movement, h_Throw;
    private SerializedProperty _data, _orbHandler, startingOrbs,
                               _moveSpeed, _turnSpeed, _moveAccel, 
                               _linearDrag,  _gravityVal, 
                               _inventorySlots, _throwCooldown, 
                               _throwForce, _throwDistance, 
                               _throwPoint;

    void OnEnable() {
        _data = serializedObject.FindProperty(nameof(_data));
        _orbHandler = serializedObject.FindProperty(nameof(_orbHandler));
        _moveSpeed = serializedObject.FindProperty(nameof(_moveSpeed));
        _turnSpeed = serializedObject.FindProperty(nameof(_turnSpeed));
        _moveAccel = serializedObject.FindProperty(nameof(_moveAccel));
        _linearDrag = serializedObject.FindProperty(nameof(_linearDrag));
        _gravityVal = serializedObject.FindProperty(nameof(_gravityVal));
        _inventorySlots = serializedObject.FindProperty(nameof(_inventorySlots));
        _throwCooldown = serializedObject.FindProperty(nameof(_throwCooldown));
        _throwForce = serializedObject.FindProperty(nameof(_throwForce));
        _throwDistance = serializedObject.FindProperty(nameof(_throwDistance));
        _throwPoint = serializedObject.FindProperty(nameof(_throwPoint));
        startingOrbs = serializedObject.FindProperty(nameof(startingOrbs));
    }

    public override void OnInspectorGUI() {
        if (!showDefaultInspector) {
            try {
                GUI.enabled = false;
                EditorGUILayout.ObjectField("Script", target as Player, typeof(Player), false);
                GUI.enabled = true;

                serializedObject.Update();

                if (h_References = EditorGUILayout.Foldout(h_References, "Reference Variables", true, EditorStyles.foldoutHeader)) {
                    EditorGUILayout.PropertyField(_data);
                    EditorGUILayout.PropertyField(_orbHandler);
                } if (h_Movement = EditorGUILayout.Foldout(h_Movement, "Movement Variables", true, EditorStyles.foldoutHeader)) {
                    EditorGUILayout.PropertyField(_moveSpeed);
                    EditorGUILayout.PropertyField(_turnSpeed);
                    EditorGUILayout.PropertyField(_moveAccel);
                    EditorGUILayout.PropertyField(_linearDrag);
                    EditorGUILayout.PropertyField(_gravityVal);
                } if (h_Throw = EditorGUILayout.Foldout(h_Throw, "Throw Variables", true, EditorStyles.foldoutHeader)) {
                    EditorGUILayout.PropertyField(_inventorySlots);
                    EditorGUILayout.PropertyField(_throwCooldown);
                    EditorGUILayout.PropertyField(_throwForce);
                    EditorGUILayout.PropertyField(_throwDistance);
                } Rect rect = EditorGUILayout.GetControlRect(false, startingOrbs.isExpanded ? EditorGUIUtility.singleLineHeight * (startingOrbs.arraySize + 3)
                                                                                            : EditorGUIUtility.singleLineHeight);
                rect = new(rect) { x = rect.x + 3, width = rect.width - 3 }; 
                EditorGUI.PropertyField(rect, startingOrbs);

                serializedObject.ApplyModifiedProperties();

            } catch {
                showDefaultInspector = true;
                GUIUtility.ExitGUI();
            }
        } else {
            EditorGUILayout.HelpBox("Showing Default Inspector", MessageType.Info);
            base.OnInspectorGUI();
        }
    }
}
