using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;


public class RandomTileFactory : ITileFactory
{
    private List<Tile> _tiles = new List<Tile>();

    public RandomTileFactory(List<Tile> tiles)
    {
        _tiles = tiles;
    }

    public Tile GetTile(Vector3Int position)
    {
        int r = Random.Range(0, _tiles.Count);
        return _tiles[r];
    }
}
