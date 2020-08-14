using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectViewer : MonoBehaviour {
    public enum ObjectType {
        CoffeeMachine,
        Leaflet
    }

    [SerializeField] public ObjectType type;
    public GameObject character;
    public GameObject interactableObject;
    public ScreenOverlayUI screenOverlay;

    PlayerBehaviour playerBehaviour; 
    bool inRange = false;
    bool movementDisabled = false;
    bool rotateable = false;

    void Awake() {
        playerBehaviour = character.GetComponent<PlayerBehaviour>();
    }

    void OnTriggerEnter(Collider other) {
        screenOverlay.SetMessageBox("Press 'F' to view " + (type == ObjectType.CoffeeMachine ? "coffee machine" : "leaflet"));
        inRange = true;
    }

    void OnTriggerExit(Collider other) {
        screenOverlay.SetMessageBox("");
        inRange = false;
    }

    // Update is called once per frame
    void Update() {
        if (inRange && Input.GetKeyDown(KeyCode.F)) {            
            if (movementDisabled == false) {
                screenOverlay.SetMessageBox("Press 'F' to put down\nPress 'Left Mouse' to rotate");
                movementDisabled = rotateable = true;
                playerBehaviour.movementEnabled = character.GetComponentInChildren<MouseLook>().movementEnabled = false;
                character.transform.localPosition = new Vector3(transform.position.x, character.transform.position.y, transform.position.z);                
                if (type == ObjectType.CoffeeMachine) {
                    interactableObject.transform.localPosition = new Vector3(interactableObject.transform.localPosition.x,
                                                                             interactableObject.transform.localPosition.y - 4.0f,
                                                                             interactableObject.transform.localPosition.z + 8.0f);                    
                    interactableObject.transform.localRotation *= Quaternion.AngleAxis(40.0f, Vector3.left);
                }
                if (type == ObjectType.Leaflet) {
                    interactableObject.transform.localPosition = new Vector3(interactableObject.transform.localPosition.x + 6.0f, 
                                                                             interactableObject.transform.localPosition.y + 15.5f, 
                                                                             interactableObject.transform.localPosition.z + 6.0f);
                    interactableObject.transform.localRotation = Quaternion.AngleAxis(33.5f, Vector3.up);
                    interactableObject.transform.localRotation *= Quaternion.AngleAxis(35.0f, Vector3.right);
                }
                character.GetComponentInChildren<MouseLook>().LookAt(interactableObject.transform.position);
            }
            else {
                movementDisabled = rotateable = false;
                playerBehaviour.movementEnabled = character.GetComponentInChildren<MouseLook>().movementEnabled = true;

                if (type == ObjectType.CoffeeMachine) {
                    interactableObject.transform.localPosition = new Vector3(interactableObject.transform.localPosition.x,
                                                                             interactableObject.transform.localPosition.y + 4.0f,
                                                                             interactableObject.transform.localPosition.z - 8.0f);                    
                    interactableObject.transform.localRotation *= Quaternion.AngleAxis(40.0f, Vector3.right);
                    interactableObject.transform.localRotation = Quaternion.AngleAxis(0.0f, Vector3.forward);
                }
                if (type == ObjectType.Leaflet) {
                    interactableObject.transform.localPosition = new Vector3(interactableObject.transform.localPosition.x - 6.0f,
                                                                             interactableObject.transform.localPosition.y - 15.5f,
                                                                             interactableObject.transform.localPosition.z - 6.0f);
                    interactableObject.transform.localRotation *= Quaternion.AngleAxis(35.0f, Vector3.right);
                    interactableObject.transform.localRotation = Quaternion.AngleAxis(40.0f, Vector3.up);
                }
            }            
        }
        if (Input.GetMouseButtonDown(0) && rotateable) {
            if (type == ObjectType.CoffeeMachine) {
                interactableObject.transform.localRotation *= Quaternion.AngleAxis(90.0f, Vector3.forward);
                if (interactableObject.transform.localRotation.eulerAngles.z == 180.0f && !character.GetComponent<PlayerBehaviour>().ContainsCode(PlayerBehaviour.ExitCodeType.Four)) {
                    playerBehaviour.AddCode(PlayerBehaviour.ExitCodeType.Four);
                    screenOverlay.SetSecondDigit();
                }
            }
            if (type == ObjectType.Leaflet) {
                interactableObject.transform.localRotation *= Quaternion.AngleAxis(180.0f, Vector3.forward);
                if (character.GetComponent<PlayerBehaviour>().ContainsKey(Key.KeyType.Leaflet)) {
                    character.GetComponent<PlayerBehaviour>().AddCode(PlayerBehaviour.ExitCodeType.Eight);
                    playerBehaviour.AddCode(PlayerBehaviour.ExitCodeType.Eight);
                    screenOverlay.SetFirstDigit();
                }
            }            
        }
    }
}