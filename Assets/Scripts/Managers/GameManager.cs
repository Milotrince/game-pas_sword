using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _enemyCount = 0;
    [SerializeField] private int _wave = 0;
    [SerializeField] private PlayerController _player;
    [SerializeField] private HealthController _health;
    [SerializeField] private Image _canvasImage;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private CameraShaker _cameraShaker;
    [SerializeField] private RectTransform _spawnableArea;


    public void SetPassword(Password password)
    {
        _player.SetPassword(password);
        _health.SetPassword(password);
        StartWave();
    }

    public void DamagePlayer(Projectile projectile, char c)
    {
        bool hit = _health.TryDamage(c, projectile.SureHit);
        if (hit)
            _cameraShaker.Shake();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Tint(new Color(0f, 0f, 0f, 0.5f));
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Tint(Color.clear);
    }

    public void AddToEnemyCount(int i)
    {
        _enemyCount += i;
        if (_enemyCount <= 0)
        {
            EndWave();
        }
    }

    public void Tint(Color color)
    {
        _canvasImage.color = color;
    }


    private void StartWave()
    {
        char[] numChars = Password.NumberChars;
        char[] lowerChars = Password.LowerChars;
        char[] upperChars = Password.UpperChars;
        char[] specialChars = Password.SpecialChars;
        char[] allChars = Password.AllChars;
        char[] letterChars = (lowerChars.ToString() + upperChars.ToString()).ToCharArray();
        char[] lowerAndNumChars = (lowerChars.ToString() + numChars.ToString()).ToCharArray();
        char[] letterAndNumChars = (lowerAndNumChars.ToString() + upperChars.ToString()).ToCharArray();

        _wave++;
        switch (_wave)
        {
            case 1:
                Projectile p1 = new Projectile(0.2f);
                Projectile p2 = new Projectile(0.1f);
                Enemy e1 = new Enemy(lowerAndNumChars, 1.5f, p1);
                Enemy e2 = new Enemy(numChars, 2.5f, p2);
                for (int i = 0; i < 2; i++)
                {
                    SpawnRandom(e1);
                    SpawnRandom(e2);
                }
                break;
            case 2:
                Projectile p3 = new Projectile(0.3f, bounces: 1);
                Projectile p4 = new Projectile(0.2f, bounces: 2);
                Enemy e3 = new Enemy(lowerAndNumChars, 2f, p3);
                Enemy e4 = new Enemy(numChars, 3f, p4, emitPattern: EnemyEmitPattern.Diagonal);
                for (int i = 0; i < 2; i++)
                {
                    SpawnRandom(e3);
                    SpawnRandom(e4);
                }
                break;
            case 3:
                Projectile p5 = new Projectile(0.3f);
                Projectile p6 = new Projectile(0.1f, track: true);
                Enemy e5 = new Enemy(lowerAndNumChars, 1f, p5);
                Enemy e6 = new Enemy(numChars, 4f, p6);
                for (int i = 0; i < 2; i++)
                {
                    SpawnRandom(e5);
                    SpawnRandom(e6);
                }
                break;
            case 4:
                Projectile p7 = new Projectile(0.1f, sureHit: true);
                Projectile p8 = new Projectile(0.08f, bounces: 1);
                Enemy e7 = new Enemy(numChars, 0.5f, p7, emitPattern: EnemyEmitPattern.Diagonal);
                Enemy e8 = new Enemy(numChars, 1f, p8, emitPattern: EnemyEmitPattern.Diagonal);
                for (int i = 0; i < 2; i++)
                {
                    SpawnRandom(e7);
                    SpawnRandom(e8);
                }
                break;
            default:
                break;
        }
    }

    private void EndWave()
    {
        OpenShop();
    }

    private void OpenShop()
    {
        StartWave();
    }

    private void SpawnRandom(Enemy enemy)
    {
        _spawner.SpawnEnemy(enemy, RandomMapPosition(), RandomDirection());
    }

    private Vector3 RandomMapPosition()
    {
        float x = _spawnableArea.position.x + Random.value * _spawnableArea.rect.width;
        float y = _spawnableArea.position.y + Random.value * _spawnableArea.rect.height;
        return new Vector3(x, y, 0f);
    }

    private Vector2 RandomDirection()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                return Vector2.up;
            case 1:
                return Vector2.down;
            case 2:
                return Vector2.right;
            default:
                return Vector2.left;
        }
    }

}
