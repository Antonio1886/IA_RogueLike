using UnityEngine;
using System.Collections.Generic;

public class CrossGenerator : IBlockGenerator
{
    private int _width, _height;
    public CrossGenerator(int width, int height)
    {
        _width = width;
        _height = height;
    }
    public MapBlock Generate(Vector3Int origin)
    {
        MapBlock map = new MapBlock();
        List<Vector3Int> positions = new List<Vector3Int>();
        Vector3Int exit = origin + new Vector3Int(_width,0,0);
        for (int i = origin.x; i < _width + origin.x; i++)
        {
            positions.Add(new Vector3Int(i, (_height / 2)+ origin.y, 0));
           
        }
        for (int j = origin.y; j < _height + origin.y; j++)
        {
            positions.Add(new Vector3Int((_width / 2) + origin.x, j, 0));
            
        }
        map.SetWidthHeight(_width, _height);
        map.SetEntryExit(origin, exit);
        map.SetTilesPositions(positions);
        return map;
    }
    /*
    // Para que cambiar algo que ya funciona? jaja
    public MapBlock Generate(Vector3Int origin)
    {
        LineGenerator line1 = new LineGenerator(_width, Vector3Int.right);
        LineGenerator line2 = new LineGenerator(_height, Vector3Int.up);

        MapBlock map = new MapBlock();

        List<Vector3Int> positions = new List<Vector3Int>();

        Vector3Int exit = origin + new Vector3Int(_width, 0, 0);

        line1.Generate(origin).TilesPositions.ForEach(pos => positions.Add(pos));
        line2.Generate(origin).TilesPositions.ForEach(pos => positions.Add(pos));

        map.SetEntryExit(origin, exit);
        map.SetTilesPositions(positions);

        return map;
    }
    */
}
