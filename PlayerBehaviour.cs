using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    public enum ExitCodeType {
        Eight,
        Four,
        One,
        Nine
    }

    List<Key.KeyType> keyList;
    List<ExitCodeType> codeList;

    public CharacterController controller;
    public ScreenOverlayUI screenOverlay;
    public GameObject overlay;
    public GameObject endScreen;
    public GameObject distortedClue;
    public GameObject revealedClue;
    public float movementSpeed = 10.0f;
    public bool movementEnabled = true;

    Key key;
    Puzzle puzzle;    
    bool interactableObject = false;
    
    void Awake() {
        keyList = new List<Key.KeyType>();
        codeList = new List<ExitCodeType>();
    }

    void Update() {
        if (movementEnabled) {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 moveDirection = transform.right * x + transform.forward * z;
            moveDirection.y -= 9.81f;
            controller.Move(moveDirection * movementSpeed * Time.deltaTime);
            
        }        

        if (interactableObject) {
            if (key != null) {
                string displayMessage = "Press 'F' ";
                if (key.GetKeyType() == Key.KeyType.Red || key.GetKeyType() == Key.KeyType.Blue) {
                    displayMessage += "to pick up the " + (key.GetKeyType() == Key.KeyType.Blue ? "blue key" : "red key");
                }
                if (key.GetKeyType() == Key.KeyType.Cable || key.GetKeyType() == Key.KeyType.Printer) {
                    displayMessage += "to pick up the " + (key.GetKeyType() == Key.KeyType.Cable ? "cable" : "screwdriver");
                }
                if(key.GetKeyType() == Key.KeyType.Wifi) {
                    displayMessage += "to turn on the Wifi";
                }
                screenOverlay.SetMessageBox(displayMessage);
                if (Input.GetKeyDown(KeyCode.F)) {
                    screenOverlay.SetMessageBox("");
                    if (key.GetKeyType() == Key.KeyType.Blue) {
                        screenOverlay.SetBlueNotAcquired();
                    }
                    if (key.GetKeyType() == Key.KeyType.Red) {
                        screenOverlay.SetRedNotAcquired();
                    }

                    AddKey(key.GetKeyType());
                    if (key.GetKeyType() != Key.KeyType.Wifi) {
                        Destroy(key.gameObject);
                    }
                    else {
                        key.GetComponent<BoxCollider>().enabled = false;
                    }
                    key = null;
                }
            }
            if(puzzle != null) {
                if (puzzle.GetPuzzleType() == Key.KeyType.Printer && !ContainsCode(ExitCodeType.Nine)) {
                    if (ContainsKey(Key.KeyType.Printer)) {
                        screenOverlay.SetMessageBox("Press 'F' to fix the printer");
                        if (Input.GetKeyDown(KeyCode.F)) {
                            AddCode(ExitCodeType.Nine);
                            screenOverlay.SetForthDigit();
                            screenOverlay.SetMessageBox("");
                        }
                    }
                    else {
                        screenOverlay.SetMessageBox("The printer is jammed, a screwdriver is needed to fix it");
                    }
                }
                if (puzzle.GetPuzzleType() == Key.KeyType.Wifi) {
                    if (ContainsKey(Key.KeyType.Wifi)) {
                        screenOverlay.SetMessageBox("The email says the leaflet has the wrong code, it should have one added to it");
                        AddKey(Key.KeyType.Leaflet);
                    }
                    else {
                        screenOverlay.SetMessageBox("The Wifi is off, the computer cannot connect to the network");
                    }
                }
                if(puzzle.GetPuzzleType() == Key.KeyType.Cable) {
                    if (ContainsKey(Key.KeyType.Cable)) {
                        if (!ContainsCode(ExitCodeType.One)) {
                            screenOverlay.SetMessageBox("Press 'F' to connect projector to the laptop");
                            if (Input.GetKeyDown(KeyCode.F)) {
                                screenOverlay.SetMessageBox("");
                                distortedClue.SetActive(false);
                                revealedClue.SetActive(true);
                                AddCode(ExitCodeType.One);
                                screenOverlay.SetThirdDigit();
                            }
                        }                                               
                    }
                    else {
                        screenOverlay.SetMessageBox("The projector needs a cable to connect it to this laptop");
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        key = other.GetComponent<Key>();
        puzzle = other.GetComponent<Puzzle>();

        if (key != null || puzzle != null) {
            interactableObject = true;
        }

        DoorController keyDoor = other.GetComponent<DoorController>();
        if (keyDoor != null) {
            if (ContainsKey(keyDoor.GetDoorType())) {
                keyDoor.ActivateDoor();
            }

            if(keyDoor.GetDoorType() == Key.KeyType.Exit && FullCode()) {
                Debug.Log("Ping");
                movementEnabled = false;
                GetComponentInChildren<MouseLook>().movementEnabled = false;
                overlay.SetActive(false);
                endScreen.SetActive(true);
            }
        }      
    }

    void OnTriggerExit(Collider other) {
        interactableObject = false;
        key = null;
        puzzle = null;
        screenOverlay.SetMessageBox("");
    }

    public void AddKey(Key.KeyType keyType) {
        keyList.Add(keyType);
    }

    public bool ContainsKey(Key.KeyType keyType) {
        return keyList.Contains(keyType);
    }

    public void AddCode(ExitCodeType codeType) {
        codeList.Add(codeType);
    }

    public bool ContainsCode(ExitCodeType codeType) {
        return codeList.Contains(codeType);
    }

    public bool FullCode() {
        return codeList.Contains(ExitCodeType.Eight) &&
               codeList.Contains(ExitCodeType.Four) &&
               codeList.Contains(ExitCodeType.One) &&
               codeList.Contains(ExitCodeType.Nine);
    }
}
