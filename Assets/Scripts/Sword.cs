using UnityEngine;
using System.Text.RegularExpressions;

public class Sword
{
    float strength;
    Vector2Int size; // oriented vertically
    string password;
    Color color;

    public Sword(string password) {
        this.password = password;
        strength = CalculateStrength(password);
        color = Color.HSVToRGB(Random.value, 1, 1);

        int width = CalculateSwordWidth(strength);
        size = new Vector2Int(width, password.Length);
    }

    public Vector2Int GetSize() {
        return size;
    }
    public string GetPassword() {
        return password;
    }
    public float GetStrength() {
        return strength;
    }
    public Color GetColor() {
        return color;
    }


    private float CalculateStrength(string pw) {
        Regex rx = new Regex("[a-z]");
        Debug.Log(rx.Match(pw));
        Debug.Log((rx.Match(pw)).GetType());

        // bool hasLetters = (new Regex(".*[a-z].*)")).Match(pw);
        // bool hasCaps = (new Regex(".*[A-Z].*)")).Match(pw);
        // bool hasLetters = (new Regex(".*[a-z].*)")).match(pw);
        // bool hasLetters = (new Regex(".*[a-z].*)")).match(pw);
        // return pw;
        return 1.0f;
    }

    private int CalculateSwordWidth(float strength) {
        return Mathf.Clamp( Mathf.RoundToInt(strength / 100f), 1, 5 );
    }
}
