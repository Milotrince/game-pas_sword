using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salt : MonoBehaviour
{
    private float _saltNum = 1.0f;
    private float _crypto = 100.0f;

    public void Add()
    {
        _saltNum += 1.0f;
        _crypto -= 10.0f;
        Debug.Log(_saltNum);
    }
}
