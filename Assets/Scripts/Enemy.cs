using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyBehavior {
    Stationary, FollowX, FollowY, Manuever
}

public enum EnemyEmitPattern {
    Forward, Diagonal, TwoDiagonal
}

public class Enemy
{

    public readonly char[] Chars;
    public readonly float EmitCooldown;
    public readonly Projectile Projectile;
    public readonly EnemyBehavior Behavior;
    public readonly EnemyEmitPattern EmitPattern;

    public Enemy(
        char[] chars,
        float emitCooldown,
        Projectile projectile,
        EnemyBehavior behavior = EnemyBehavior.Stationary,
        EnemyEmitPattern emitPattern = EnemyEmitPattern.Forward)
    {
        Chars = chars;
        EmitCooldown = emitCooldown;
        Projectile = projectile;
        Behavior = behavior;
        EmitPattern = emitPattern;
    }

}
