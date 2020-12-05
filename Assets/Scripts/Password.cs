
using UnityEngine;
using System.Text.RegularExpressions;

/// <remarks>
/// I pray that nobody looks at this class out of context.
/// </remarks>
public class Password
{
    public readonly string String;

    public Password(string password) {
        String = password;
    }

    /// <returns>
    /// vector2 in "scientific notation" since actual number can get very large.
    /// x * 10^y
    /// </returns>
    public Vector2 CalculateCombinations() {
        int chars = 10 + 26 + 26 + 33;
        float e = String.Length*Mathf.Log10(chars);
        float y = Mathf.Floor(e);
        float x = Mathf.Pow(10, e - y);
        return new Vector2(x, y);
    }

    /// <returns>
    /// 0f to 1f
    /// </returns>
    public float CalculateComplexity() {
        float charpool = 0f;
        float maxCharpool = 10f + 26f + 26f + 33f;
        if (HasNumbers())
            charpool += 10;
        if (HasLowerLetters())
            charpool += 26;
        if (HasUpperLetters())
            charpool += 26;
        if (HasSpecial())
            charpool += 33;
        float charpoolComplexity = charpool/maxCharpool;

        return charpoolComplexity;
    }

    public bool HasNumbers() {
        return (new Regex("[0-9])")).IsMatch(String);
    }

    public bool HasLowerLetters() {
        return (new Regex("[a-z])")).IsMatch(String);
    }

    public bool HasUpperLetters() {
        return (new Regex("[A-Z])")).IsMatch(String);
    }

    public bool HasSpecial() {
        return (new Regex("[ -/:-@[-`{-~]")).IsMatch(String);
    }

}