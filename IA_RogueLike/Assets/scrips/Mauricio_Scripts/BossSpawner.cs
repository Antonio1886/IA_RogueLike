using UnityEngine;

public class BossSpawner : MonoBehaviour, IEnemySpawner
{
    public GameObject bossPrefab;

    public void SpawnEnemies(Vector2Int roomPosition, Transform parent)
    {
        // Calcula la posición del spawn en el centro de la sala (asumiendo sala de 20x20 unidades)
        Vector3 spawnPos = new Vector3(
            roomPosition.x * 20 + 10,
            roomPosition.y * 20 + 10,
            0
        );

        Instantiate(bossPrefab, spawnPos, Quaternion.identity, parent);
    }
}

