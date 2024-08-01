using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    [SerializeField] private float moveAmount = 2f; // The amount to move up
    [SerializeField] private float duration = 1f; // The duration of the movement
    private Vector3 startPosition; // The initial position of the object
    private Vector3 targetPosition; // The target position of the object
    private bool isMoving = false; // Flag to indicate if the object is currently moving
    private float elapsedTime = 0f; // Timer to track the elapsed time

    void Update() {
        // Check for the key press
        if (Input.GetKeyDown(KeyCode.T) && !isMoving) {
            // Set the start and target positions
            startPosition = transform.position;
            targetPosition = startPosition + new Vector3(0, moveAmount, 0);

            // Reset the timer and set the moving flag
            elapsedTime = 0f;
            isMoving = true;
        }

        // Move the object if it's currently moving
        if (isMoving) {
            // Update the elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the lerp factor (clamped between 0 and 1)
            float lerpFactor = Mathf.Clamp01(elapsedTime / duration);

            // Interpolate the position
            transform.position = Vector3.Lerp(startPosition, targetPosition, lerpFactor);

            // Check if the movement is complete
            if (lerpFactor >= 1f) {
                isMoving = false;
            }
        }
    }
}
