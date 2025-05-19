using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
public class PerlinNoiseFactory : ITileFactory
{
    private List<Tile> _tiles = new List<Tile>();

    public PerlinNoiseFactory(List<Tile> tiles)
    {
        _tiles = tiles;
    }

    public Tile GetTile(Vector3Int position)
    {
        float scale = 0.1f; 
        float offsetX = 100f; 
        float offsetY = 100f;

        float perlinValue = Mathf.PerlinNoise((position.x + offsetX) * scale, (position.y + offsetY) * scale);

        int tileIndex = Mathf.FloorToInt(perlinValue * _tiles.Count);

        return _tiles[tileIndex];
    }

}
