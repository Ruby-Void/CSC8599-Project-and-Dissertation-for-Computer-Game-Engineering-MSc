using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuUI : MonoBehaviour {
    public Camera playerCamera;
    public Camera startMenuCamera;
    public PlayerBehaviour playerBehaviour;
    public GameObject startMenu;
    public GameObject screenOverlay;
    
    // Start is called before the first frame update
    void Awake() {
        startMenuCamera.enabled = true;
        playerCamera.enabled = false;
        playerBehaviour.movementEnabled = false;
    }
    
    public void StartGame() {
        startMenuCamera.enabled = false;
        playerCamera.enabled = true;
        playerBehaviour.movementEnabled = true;
        startMenu.SetActive(false);
        screenOverlay.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
