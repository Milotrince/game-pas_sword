using UnityEngine;

public class Sword
{
    public readonly float BaseDamage;
    public readonly float SwingSpeed;
    public readonly float SwingCooldown;
    public readonly float SwingRange;
    public readonly Password Password;

    public Sword(Password password)
    {
        Password = password;
        // TODO: make not arbitrary calculation
        BaseDamage = password.CalculateCombinations().y;
        SwingSpeed = 1f;
        SwingCooldown = 0.1f;
        SwingRange = Mathf.PI/4f;
    }

}
