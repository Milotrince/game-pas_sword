using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _enemyCount = 0;
    [SerializeField] private int _wave = 0;
    [SerializeField] private PlayerController _player;
    [SerializeField] private Image _canvasImage;
    [SerializeField] private EnemySpawner _spawner;


    public void SetPassword(Password password)
    {
        _player.SetPassword(password);
        StartWave();
    }

    public void DamagePlayer(Projectile projectile)
    {
        Debug.Log("DamagePlayer");
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
        _wave++;
        switch (_wave)
        {
            case 1:
                Projectile projectile = new Projectile(0.2f, bounces: 2);
                Enemy enemy = new Enemy(Password.NumberChars, 1f, projectile);
                _spawner.SpawnEnemy(enemy);
                break;
        }
    }

    private void EndWave()
    {
        OpenShop();
    }

    private void OpenShop()
    {
        Debug.Log("DamagePlayer");
    }

}
