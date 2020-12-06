using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pepper : MonoBehaviour
{
    private float _pepperNum = 1.0f;
    private float _crypto = 100.0f;

    public void Add()
    {
        _pepperNum += 1.0f;
        _crypto -= 10.0f;
        Debug.Log(_pepperNum);
    }
}
