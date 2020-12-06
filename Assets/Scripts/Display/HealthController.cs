using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public GameObject CharPrefab;
    public Transform CharLayout;
    public float PercentCompromised { get; }
    private Password _password;
    private char[] PasswordState;
    private Text[] _charTexts;


    public bool TryDamage(char c, bool surefire = false)
    {
        int i = -1;
        for (int j = 0; j < PasswordState.Length; j++)
        {
            if (PasswordState[j] == c)
                i = j;
        }
        if (i > -1)
        {
            float chance = surefire ? 1f : Mathf.Clamp01(1f - _password.ComplexityScore + 1f/_password.String.Length);
            if (Random.Range(0f, 1f) < chance)
                CompromiseCharAt(i);
            return true;
        }
        return false;
    }

    private void CompromiseCharAt(int i)
    {
        PasswordState[i] = ' ';
        _charTexts[i].color = Color.red;
    }

    public void SetPassword(Password password)
    {
        PasswordState = password.String.ToCharArray();
        _password = password;

        _charTexts = new Text[password.String.Length];
        foreach (Transform child in CharLayout)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < password.String.Length; i++) 
        {
            GameObject charObject = Instantiate(CharPrefab, CharLayout);
            Text text = charObject.GetComponentInChildren<Text>() as Text;
            text.text = password.String[i].ToString();
            _charTexts[i] = text;
        }
    }

}
