using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectileController : ProjectileController
{
    private Vector3 _velocity = new Vector3();

    protected override void Initialize() {
        float v = _projectile.Speed;
        _velocity.x = v * Mathf.Cos(_angle);
        _velocity.y = v * Mathf.Sin(_angle);
        _velocity.z = 0f;
    }

    protected override void UpdateProjectile() {
        transform.position += _velocity;
    }
}
