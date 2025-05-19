using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBlock 
{
    public List<Vector3Int> TilesPositions { get; private set; } = new List<Vector3Int>();
    public Vector3Int EntryPoint { get; private set; }
    public Vector3Int ExitPoint { get; private set; }

    private ITileFactory _tileFactory;

    public int width, height;


    public void SetTilesPositions(List<Vector3Int> positions)
    {
        TilesPositions = positions;
    }

    public void SetEntryExit(Vector3Int entry, Vector3Int exit)
    {
        EntryPoint = entry;
        ExitPoint = exit;
    }
    public void SetWidthHeight(int width, int height)
    {
        this.width = width;
        this.height = height;
    }
    public void SetTileFactory(ITileFactory tileFactory)
    {
        _tileFactory = tileFactory;
    }
    public List<Tile> GenerateTilesArray()
    {
        List<Tile> tiles = new List<Tile>();
        foreach (Vector3Int position in TilesPositions)
        {
            tiles.Add(_tileFactory.GetTile(position));
            //tiles.Add(tile);
        }
        return tiles;
    }
}
