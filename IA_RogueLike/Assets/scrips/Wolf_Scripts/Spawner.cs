using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
   public GameObject[] enemyPrefabs; // Array con los diferentes tipos de enemigos
    public Transform[] spawnPoints; // Array de posiciones donde pueden aparecer

    public float spawnInterval = 2f; // Tiempo entre spawns

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            int enemyIndex = Random.Range(0, enemyPrefabs.Length); // Selecciona un enemigo al azar
            int spawnIndex = Random.Range(0, spawnPoints.Length); // Selecciona una posición al azar

            Instantiate(enemyPrefabs[enemyIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
        }
    }
}
