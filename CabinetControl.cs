using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetControl : MonoBehaviour
{
    public ScreenOverlayUI screenOverlay;
    public GameObject door;
    
    public bool doorOpen = false;
    private bool inRange;

    void OpenDoor() {
        doorOpen = true;
        door.GetComponent<Animator>().SetBool("open", true);
    }

    void CloseDoor() {
        doorOpen = false;
        door.GetComponent<Animator>().SetBool("open", false);
    }

    void OnTriggerEnter(Collider other) {
        screenOverlay.SetMessageBox("Press 'F' to open cabinet");
        inRange = true;
    }

    void OnTriggerExit(Collider other) {
        screenOverlay.SetMessageBox("");
        inRange = false;
        CloseDoor();
    }

    void Update() {
        if (inRange && Input.GetKeyDown(KeyCode.F)) {
            OpenDoor();
        }
    }
}
