using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    public Transform playerBody;
    public float mouseSensetivity = 100.0f;
    public bool movementEnabled = true;
    float xRotation = 0.0f;

    public void LookAt(Vector3 targetObject) {
        transform.LookAt(targetObject);
    }

    // Update is called once per frame
    void Update() {
        if (movementEnabled) {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);
            transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);

            playerBody.Rotate(Vector3.up * mouseX);
        }        
    }
}
