using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Sprite[] EnemySprites;

    public void SpawnEnemy(Enemy enemy) {
        GameObject enemyObject = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        EnemyController controller = enemyObject.AddComponent<EnemyController>() as EnemyController;
        controller.SetEnemy(enemy);
    }

}
