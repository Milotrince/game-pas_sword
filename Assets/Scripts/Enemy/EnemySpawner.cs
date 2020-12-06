using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Sprite[] EnemySprites;

    public void SpawnEnemy(Enemy enemy, Vector3 position, Vector2 direction) {
        GameObject enemyObject = Instantiate(EnemyPrefab, position, Quaternion.identity);
        EnemyController controller = enemyObject.AddComponent<EnemyController>() as EnemyController;
        controller.SetEnemy(enemy, direction);
    }

}
