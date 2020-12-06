using UnityEngine;

public class Sword
{
    public readonly float SwingSpeed;
    public readonly float SwingAngle;
    public readonly Password Password;

    public Sword(Password password)
    {
        Password = password;
        // TODO: not arbitrary number
        SwingSpeed = 10f;
        SwingAngle = 180f;
    }

}
