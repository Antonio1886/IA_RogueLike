using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
public class ChessMapFactory : ITileFactory
{
    private List<Tile> _tiles = new List<Tile>();


    public ChessMapFactory(List<Tile> tiles)
    {
        _tiles = tiles;
    }

    public Tile GetTile(Vector3Int position)
    {
        if ((position.x + position.y) % 2 == 0)
        {
            return _tiles[0];
        }
        else
        {
            return _tiles[1];

        }
    }
}

