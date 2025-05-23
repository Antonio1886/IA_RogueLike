using UnityEngine;
using System.Collections.Generic;

public class FrameGenerator : IBlockGenerator
{
    private int _width, _height, _holeWidth, _holeHeight;

    public FrameGenerator(int width, int height, int holeWidth, int holeHeight)
    {
        _width = width;
        _height = height;
        _holeWidth = holeWidth;
        _holeHeight = holeHeight;
    }

    public MapBlock Generate(Vector3Int origin)
    {
        MapBlock map = new MapBlock();
        List<Vector3Int> positions = new List<Vector3Int>();
        Vector3Int exit = origin + new Vector3Int(_width, _height, 0);
        for (int i = origin.x; i < _width + origin.x; i++)
        {
            for (int j = origin.y; j < _height + origin.y; j++)
            {
                positions.Add(new Vector3Int(i, j, 0));
            }
        }
        for (int i = origin.x + _width/4; i < _holeWidth + origin.x + _width / 4; i++)
        {
            for (int j = origin.y + _height/4; j < _holeHeight + origin.y + _height / 4; j++)
            {
                positions.Remove(new Vector3Int(i, j, 0));
            }
        }
        map.SetWidthHeight(_width, _height);
        map.SetEntryExit(origin, exit);
        map.SetTilesPositions(positions);
        return map;
    }
}

