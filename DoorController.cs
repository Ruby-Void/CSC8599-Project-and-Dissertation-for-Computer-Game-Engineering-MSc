#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Key.KeyType doorType;

    public CharacterController player;
    public GameObject door;
    private bool doorActive = false;

    public Key.KeyType GetDoorType() {
        return doorType;
    }

    public void ActivateDoor() {
        doorActive = true;
        OpenDoor();
    }

    void OpenDoor() {
        door.GetComponent<Animator>().SetBool("open", true);
    }

    void CloseDoor() {
        door.GetComponent<Animator>().SetBool("open", false);
    }

    void OnTriggerEnter(Collider other) {
        if (doorActive) {
            OpenDoor();
        }
    }

    void OnTriggerExit(Collider other) {
        if (doorActive) {
            CloseDoor();
        }        
    }
}
