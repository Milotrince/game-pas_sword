using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameManager _gm;
    private bool _alive;
    private Enemy _enemy;
    private Vector2 _direction;

    void Start()
    {
        _gm = FindObjectOfType<GameManager>() as GameManager;
        _gm.AddToEnemyCount(1);
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemy = enemy;
        _direction = Vector2.right;
        _alive = true;

        switch (_enemy.Behavior)
        {
            case EnemyBehavior.Stationary:
                StartCoroutine(StationaryBehavior());
                break;
            case EnemyBehavior.FollowX:
                StartCoroutine(FollowXBehavior());
                break;
            case EnemyBehavior.FollowY:
                StartCoroutine(FollowYBehavior());
                break;
            case EnemyBehavior.Manuever:
                StartCoroutine(ManueverBehavior());
                break;
        }
    }

    public void Die()
    {
        _gm.AddToEnemyCount(-1);
        Destroy(gameObject);
    }

    private IEnumerator StationaryBehavior()
    {
        while (_alive)
        {
            EmitProjectile();
            yield return new WaitForSeconds(_enemy.EmitCooldown);
        }
        Die();
    }

    private IEnumerator FollowXBehavior()
    {
        float lastEmitTime = float.MinValue;
        while (_alive)
        {
            if (Time.time - lastEmitTime < _enemy.EmitCooldown)
            {
                EmitProjectile();
                lastEmitTime = Time.time;
            }
            // transform.position += Vector3.up * _enemy.;
            yield return new WaitForFixedUpdate();
        }
        Die();
    }

    private IEnumerator FollowYBehavior()
    {
        yield return null;
    }

    private IEnumerator ManueverBehavior()
    {
        yield return null;
    }

    private void EmitProjectile() {
        GameObject projectileObject = new GameObject("Projectile");
        projectileObject.transform.position = transform.position;

        ProjectileController controller;
        if (_enemy.Projectile.Track)
            controller = projectileObject.AddComponent<TrackingProjectileController>() as ProjectileController;
        else
            controller = projectileObject.AddComponent<SimpleProjectileController>() as ProjectileController;

        int randomCharIndex = Mathf.RoundToInt(Random.Range(0,_enemy.Chars.Length-1));
        char randomChar = _enemy.Chars[randomCharIndex];
        float angle = Vector2.Angle(Vector2.right, _direction);
        controller.Initialize(_enemy.Projectile, randomChar, angle, _gm);
    }


}
