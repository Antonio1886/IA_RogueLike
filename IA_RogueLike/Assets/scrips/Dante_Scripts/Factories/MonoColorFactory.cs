using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class MonoColorFactory : ITileFactory
{
    private List<Tile> _tiles = new List<Tile>();
    private int _selectedTile;

    public MonoColorFactory(List<Tile> tiles, int selectedTile)
    {
        _tiles = tiles;
        _selectedTile = selectedTile;
    }

    public Tile GetTile(Vector3Int position)
    {
        return _tiles[_selectedTile];
    }
}
