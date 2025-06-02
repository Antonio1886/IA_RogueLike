using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] bossPrefabs;

    public int minEnemies = 1;
    public int maxEnemies = 4;

    public void SpawnEnemiesInRoom(Vector2Int roomPosition, Transform parent)
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
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity, parent);
        }
    }

    public void SpawnBossInRoom(Vector2Int roomPosition, Transform parent)
    {
        if (bossPrefabs.Length == 0)
        {
            Debug.LogWarning("No hay prefabs de jefes asignados.");
            return;
        }

        Vector3 spawnPos = new Vector3(
            roomPosition.x * 20 + 10, // posición fija o centrada
            roomPosition.y * 20 + 10,
            0
        );

        GameObject bossPrefab = bossPrefabs[Random.Range(0, bossPrefabs.Length)];
        Instantiate(bossPrefab, spawnPos, Quaternion.identity, parent);
    }
}
