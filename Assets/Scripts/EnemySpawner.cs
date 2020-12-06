using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    void Start()
    {
        char[] chars = new char[3] {'a', 'b', 'c'};
        Projectile projectile = new Projectile(0.2f, track: true);
        Enemy enemy = new Enemy(chars, 0.2f, projectile);
        SpawnEnemy(enemy);
    }

    public void SpawnEnemy(Enemy enemy) {
        // GameObject enemyObject = new GameObject();
        GameObject enemyObject = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        EnemyController controller = enemyObject.AddComponent<EnemyController>() as EnemyController;
        controller.SetEnemy(enemy);
    }
}
