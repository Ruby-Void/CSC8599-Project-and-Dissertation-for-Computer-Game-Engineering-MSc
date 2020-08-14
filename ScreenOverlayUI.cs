using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenOverlayUI : MonoBehaviour
{
    public TextMeshProUGUI messageBox;

    public TextMeshProUGUI redNotAcquired;
    public TextMeshProUGUI blueNotAcquired;

    public TextMeshProUGUI firstDigit;
    public TextMeshProUGUI secondDigit;
    public TextMeshProUGUI thirdDigit;
    public TextMeshProUGUI forthDigit;

    public void SetMessageBox(string message) {
        messageBox.SetText(message);
    }

    public void SetRedNotAcquired() {
        redNotAcquired.SetText("Acquired");
    }

    public void SetBlueNotAcquired() {
        blueNotAcquired.SetText("Acquired");
    }

    public void SetFirstDigit() {
        firstDigit.SetText("8");
    }

    public void SetSecondDigit() {
        secondDigit.SetText("4");
    }

    public void SetThirdDigit() {
        thirdDigit.SetText("1");
    }

    public void SetForthDigit() {
        forthDigit.SetText("9");
    }

    // Start is called before the first frame update
    void Start() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
