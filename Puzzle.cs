#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private Key.KeyType puzzleType;

    public Key.KeyType GetPuzzleType() {
        return puzzleType;
    }
}
