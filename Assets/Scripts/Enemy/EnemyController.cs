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

    public void SetEnemy(Enemy enemy, Vector2 direction)
    {
        _enemy = enemy;
        _direction = direction;
        _alive = true;

        switch (_enemy.Behavior)
        {
            case EnemyBehavior.Stationary:
                StartCoroutine(StationaryBehavior());
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
        yield return new WaitForSeconds(1f);
        while (_alive)
        {
            EmitProjectile();
            yield return new WaitForSeconds(_enemy.EmitCooldown);
        }
        Die();
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
        if (_enemy.EmitPattern == EnemyEmitPattern.Diagonal)
        {
            angle += 45;
        }

        controller.Initialize(_enemy.Projectile, randomChar, angle, _gm);
    }


}
