using System.Collections.Generic;
using UnityEngine;


public class RectangleGenerator : IBlockGenerator
{
    private int _width, _height;

    public RectangleGenerator(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public MapBlock Generate(Vector3Int origin)
    {
        MapBlock map = new MapBlock();
        List<Vector3Int> positions = new List<Vector3Int>();
        Vector3Int exit = new Vector3Int(_width,_height,0);
        for (int i = origin.x; i < _width + origin.x; i++)
        {
            for (int j = origin.y; j < _height + origin.y; j++)
            {
                positions.Add(new Vector3Int(i, j, 0));
            }
        }
        map.SetWidthHeight(_width, _height);
        map.SetEntryExit(origin,exit);
        map.SetTilesPositions(positions);
        return map;
    }
}
