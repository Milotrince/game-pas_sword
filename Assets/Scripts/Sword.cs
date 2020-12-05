using UnityEngine;

public class Sword
{
    public readonly float BaseDamage;
    public readonly float SwingSpeed;
    public readonly float SwingAngle;
    public readonly Password Password;

    public Sword(Password password)
    {
        Password = password;
        // TODO: make not arbitrary calculation
        BaseDamage = password.CalculateCombinations().y;
        SwingSpeed = 10f;
        SwingAngle = 120f;
    }

}
