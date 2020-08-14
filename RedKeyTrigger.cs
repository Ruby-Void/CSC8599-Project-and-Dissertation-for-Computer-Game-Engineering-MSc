using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKeyTrigger : MonoBehaviour
{
    public CabinetControl cabinetControl;
    public BoxCollider redKeyBox;

    // Update is called once per frame
    void Update() {
        if(cabinetControl.doorOpen == true) {
            redKeyBox.enabled = true;
        }
        else {
            redKeyBox.enabled = false;
        }
    }
}
