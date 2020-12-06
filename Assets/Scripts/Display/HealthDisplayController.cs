using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayController : MonoBehaviour
{
    public GameObject PINCharacterPrefab;
    public Transform PINCharacterLayout;
    private string _pin = "";
    private int _pinLength = 4;
    private Text[] _charTexts;

    void Start()
    {
        for (int i = 0; i < _pinLength; i++) 
        {
            GameObject charObject = Instantiate(PINCharacterPrefab, PINCharacterLayout);
            Text text = charObject.GetComponentInChildren<Text>() as Text;
            _charTexts[i] = text;
        }
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
