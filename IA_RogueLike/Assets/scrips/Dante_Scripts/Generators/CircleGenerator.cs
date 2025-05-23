using UnityEngine;
using System.Collections.Generic;

public class CircleGenerator : IBlockGenerator
{
    private int _radius;
    public CircleGenerator(int radius)
    {
        _radius = radius;
    }
    public MapBlock Generate(Vector3Int origin)
    {
        MapBlock map = new MapBlock();
        List<Vector3Int> positions = new List<Vector3Int>();
        Vector3Int exit = origin + new Vector3Int(_radius, _radius, 0);
        //TODO podria mover directamente origin + radius
        for (int x = -_radius; x <= _radius; x++)
        {
            for (int y = -_radius; y <= _radius; y++)
            {
                if (x * x + y * y <= _radius * _radius)
                {
                    positions.Add(new Vector3Int(origin.x + x, origin.y + y, 0));
                }
            }
        }
        map.SetWidthHeight(_radius, _radius);
        map.SetEntryExit(origin, exit);
        map.SetTilesPositions(positions);
        return map;
    }
}
