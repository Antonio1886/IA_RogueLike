using UnityEngine;

public interface IEnemySpawner
{
    void SpawnEnemies(Vector2Int roomPosition, Transform parent);
}

