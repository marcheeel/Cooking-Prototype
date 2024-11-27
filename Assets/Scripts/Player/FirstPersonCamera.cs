using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform player;

    private bool lockedCursor = true;

    public float mouseSensitivity = 2f;
    private float cameraVerticalRotation = 0f;

    private void Update()
    {
        if (lockedCursor)
        { 
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraVerticalRotation -= inputY;

        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * inputX);
    }
}
