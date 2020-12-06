public class Projectile
{
    public readonly float Speed;
    public readonly bool SureHit;
    public readonly bool Track;
    public readonly int Bounces;

    public Projectile(float speed, bool sureHit = false, bool track = false, int bounces = 0) {
        Speed = speed;
        SureHit = sureHit;
        Track = track;
        Bounces = bounces;
    }
    
}
