using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PINSelectManager : MonoBehaviour
{

    public Text TopText;
    public GameObject PINCharacterPrefab;
    public Transform PINCharacterLayout;
    private Keyboard _kb;
    private string _pin = "";
    private int _pinLength = 4;
    private Text[] _charTexts;
    private GameManager _gm;

    void Start()
    {
        _kb = Keyboard.current;

        _gm = FindObjectOfType<GameManager>() as GameManager;
        _gm.PauseGame();

        _charTexts = new Text[_pinLength];

        for (int i = 0; i < _pinLength; i++) 
        {
            GameObject charObject = Instantiate(PINCharacterPrefab, PINCharacterLayout);
            Text text = charObject.GetComponentInChildren<Text>() as Text;
            _charTexts[i] = text;
        }
    }

    void Update()
    {
        // disgusting, I know
        if (_kb.digit0Key.wasPressedThisFrame || _kb.numpad0Key.wasPressedThisFrame)
            PressedKey("0");
        if (_kb.digit1Key.wasPressedThisFrame || _kb.numpad1Key.wasPressedThisFrame)
            PressedKey("1");
        if (_kb.digit2Key.wasPressedThisFrame || _kb.numpad2Key.wasPressedThisFrame)
            PressedKey("2");
        if (_kb.digit3Key.wasPressedThisFrame || _kb.numpad3Key.wasPressedThisFrame)
            PressedKey("3");
        if (_kb.digit4Key.wasPressedThisFrame || _kb.numpad4Key.wasPressedThisFrame)
            PressedKey("4");
        if (_kb.digit5Key.wasPressedThisFrame || _kb.numpad5Key.wasPressedThisFrame)
            PressedKey("5");
        if (_kb.digit6Key.wasPressedThisFrame || _kb.numpad6Key.wasPressedThisFrame)
            PressedKey("6");
        if (_kb.digit7Key.wasPressedThisFrame || _kb.numpad7Key.wasPressedThisFrame)
            PressedKey("7");
        if (_kb.digit8Key.wasPressedThisFrame || _kb.numpad8Key.wasPressedThisFrame)
            PressedKey("8");
        if (_kb.digit9Key.wasPressedThisFrame || _kb.numpad9Key.wasPressedThisFrame)
            PressedKey("9");

    }

    private void PressedKey(string num)
    {
        if (_pin.Length >= 4)
            return;

        _charTexts[_pin.Length].text = num;
        _pin += num;

        if (_pin.Length == 4)
            StartCoroutine(Close());
    }

    private IEnumerator Close()
    {
        TopText.text = "OK";
        yield return new WaitForSecondsRealtime(1f);
        _gm.SetPassword(new Password(_pin));
        _gm.ResumeGame();
        Destroy(gameObject);
    }

}
