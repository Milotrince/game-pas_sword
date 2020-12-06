using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectileController : ProjectileController
{
    public float Lifespan = 5f;
    public float VelocityCorrectionDelay = 1f;
    private Vector3 _velocity = new Vector3();
    private Transform target;
    private float _lastCorrected = float.MinValue;
    private float _startTime;

    protected override void Initialize()
    {
        target = FindObjectOfType<PlayerController>().transform;
        _startTime = Time.time;
    }

    protected override void UpdateProjectile()
    {
        float elapsed = Time.time - _startTime;
        float alpha = Mathf.Clamp01(1.5f - Mathf.Pow(elapsed / Lifespan, 0.5f));
        Debug.Log(alpha);
        Color c = _spriteRenderer.color;
        _spriteRenderer.color = new Color(c.r, c.g, c.b, alpha);
        if (elapsed > Lifespan)
        {
            Destroy(gameObject);
            return;
        }

        if (Time.time - _lastCorrected > VelocityCorrectionDelay)
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
