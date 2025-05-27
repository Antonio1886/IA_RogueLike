using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public Transform player;
    public List<GameObject> enemyPrefabs;
    public Vector2Int salaSize = new Vector2Int(16, 9); // Tamaño de sala

    private HashSet<Vector2Int> salasYaSpawneadas = new();
    private Vector2Int salaAnterior;
    private List<Vector3> posicionesSpawneadas = new();

    void Update()
    {
        Vector3Int tilePos = tilemap.WorldToCell(player.position);
        Vector2Int salaActual = new(
            Mathf.FloorToInt((float)tilePos.x / salaSize.x),
            Mathf.FloorToInt((float)tilePos.y / salaSize.y)
        );

        if (salaActual != salaAnterior)
        {
            Debug.Log($"[EnemySpawner] Nueva sala detectada: {salaActual}");

            if (!salasYaSpawneadas.Contains(salaActual))
            {
                SpawnEnemigosEnSala(salaActual);
                salasYaSpawneadas.Add(salaActual);
            }

            salaAnterior = salaActual;
        }

        // DEBUG VISUAL: dibujar límites de la sala actual
        Vector3 esquinaInferiorIzq = new Vector3(salaActual.x * salaSize.x, salaActual.y * salaSize.y, 0);
        Vector3 esquinaSuperiorDer = esquinaInferiorIzq + new Vector3(salaSize.x, salaSize.y, 0);

        Debug.DrawLine(esquinaInferiorIzq, esquinaInferiorIzq + Vector3.right * salaSize.x, Color.green);
        Debug.DrawLine(esquinaInferiorIzq, esquinaInferiorIzq + Vector3.up * salaSize.y, Color.green);
        Debug.DrawLine(esquinaSuperiorDer, esquinaSuperiorDer - Vector3.right * salaSize.x, Color.green);
        Debug.DrawLine(esquinaSuperiorDer, esquinaSuperiorDer - Vector3.up * salaSize.y, Color.green);

        // DEBUG VISUAL: dibujar rayos en las posiciones donde spawnearon enemigos
        foreach (var pos in posicionesSpawneadas)
        {
            Debug.DrawRay(pos, Vector3.up * 0.5f, Color.red);
        }
    }

    void SpawnEnemigosEnSala(Vector2Int sala)
    {
        List<Vector3> posiblesPosiciones = new();
        posicionesSpawneadas.Clear();

        for (int x = 0; x < salaSize.x; x++)
        {
            for (int y = 0; y < salaSize.y; y++)
            {
                Vector3Int pos = new Vector3Int(
                    sala.x * salaSize.x + x,
                    sala.y * salaSize.y + y,
                    0
                );

                if (tilemap.HasTile(pos))
                {
                    posiblesPosiciones.Add(tilemap.CellToWorld(pos) + new Vector3(0.5f, 0.5f, 0));
                }
            }
        }

        int cantidadEnemigos = Random.Range(3, 7); // Entre 3 y 6 enemigos

        for (int i = 0; i < cantidadEnemigos; i++)
        {
            if (posiblesPosiciones.Count == 0) break;

            int index = Random.Range(0, posiblesPosiciones.Count);
            int enemyIndex = Random.Range(0, enemyPrefabs.Count);

            Vector3 spawnPos = posiblesPosiciones[index];
            Instantiate(enemyPrefabs[enemyIndex], spawnPos, Quaternion.identity);
            posicionesSpawneadas.Add(spawnPos);

            posiblesPosiciones.RemoveAt(index);
        }

        Debug.Log($"[EnemySpawner] Spawneados {cantidadEnemigos} enemigos en sala {sala}");
    }
}