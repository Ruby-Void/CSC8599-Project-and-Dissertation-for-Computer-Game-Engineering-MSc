#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType key;
    public enum KeyType {
        Red,
        Blue,
        Cable,
        Wifi,
        Leaflet,
        Printer,
        Exit
    }

    public KeyType GetKeyType() {
        return key;
    }
}
