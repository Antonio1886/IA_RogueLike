using UnityEngine;

public class EnemySpawner : IEnemySpawner
{
    private GameObject[] enemyPrefabs;
    private int minEnemies;
    private int maxEnemies;

    public EnemySpawner(GameObject[] enemyPrefabs, int minEnemies = 1, int maxEnemies = 4)
    {
        this.enemyPrefabs = enemyPrefabs;
        this.minEnemies = minEnemies;
        this.maxEnemies = maxEnemies;
    }

    public void SpawnEnemies(Vector2Int roomPosition, Transform parent)
    {
        int numEnemies = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 spawnPos = new Vector3(
                roomPosition.x * 20 + Random.Range(2, 18),
                roomPosition.y * 20 + Random.Range(2, 18),
                0
            );

            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Object.Instantiate(enemyPrefab, spawnPos, Quaternion.identity, parent);
        }
    }
}


