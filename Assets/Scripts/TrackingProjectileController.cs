using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectileController : ProjectileController
{
    private Vector3 _velocity = new Vector3();
    private Transform target;
    private float _velocityCorrectionDelay = 1f;
    private float _lifespan = 5f;
    private float _lastCorrected = float.MinValue;
    private float _startTime;

    protected override void Initialize()
    {
        target = FindObjectOfType<PlayerController>().transform;
        _startTime = Time.time;
    }

    protected override void UpdateProjectile()
    {
        if (Time.time - _startTime > _lifespan)
        {
            Destroy(gameObject);
            return;
        }

        if (Time.time - _lastCorrected > _velocityCorrectionDelay)
        {
            UpdateVelocity();
            _lastCorrected = Time.time;
        }
        transform.position += _velocity;
    }

    private void UpdateVelocity()
    {
        _angle = CalculateAngleToTarget();
        float v = _projectile.Speed;
        _velocity.x = v * Mathf.Cos(_angle);
        _velocity.y = v * Mathf.Sin(_angle);
        _velocity.z = 0f;
    }

    private float CalculateAngleToTarget() {
        float dx = target.position.x - transform.position.x;
        if (Mathf.Abs(dx) < Mathf.Epsilon)
            return 0;
        float dy = target.position.y - transform.position.y;
        return Mathf.Atan2(dy, dx);
    }
}
