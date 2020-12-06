using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PWDisplayer : MonoBehaviour
{
    // private int _minSlots = 4;
    private Password pw = new Password("1234");
    // private int _currentSlots = pw.Length;
    // private Password pw = new Password("1234");

    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        Debug.Log("hi");
        // Instantiate at position (0, 0, 0) and zero rotation.
        for(int i = 0; i > pw.String.Length; i++)
        {
            Instantiate(myPrefab, new Vector3(0+i+4, 0, 0), Quaternion.identity);
        }
    }
}