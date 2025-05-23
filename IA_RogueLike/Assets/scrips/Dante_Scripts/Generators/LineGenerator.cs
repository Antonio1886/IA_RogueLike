using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class LineGenerator : IBlockGenerator
{
    private int _length;
    //Este line Generator tambien cuenta como un diagonal generator
    //Pues la dirección puede ser diagonal e incluso negativa
    private Vector3Int _direction;

    public LineGenerator(int length, Vector3Int direction)
    {
        _length = length;
        _direction = direction;
    }

    public MapBlock Generate(Vector3Int origin)
    {
        MapBlock map = new MapBlock();
        List<Vector3Int> positions = new List<Vector3Int>();
        Vector3Int exit = origin + (_direction * _length);
        //Vector3Int exit = new Vector3Int(0,0,0);
        for (int i = 0; i < _length; i++)
        {
            positions.Add(origin + _direction * i);
        }
        map.SetWidthHeight(_length * _direction.x, _length * _direction.y);
        map.SetEntryExit(origin, exit);
        map.SetTilesPositions(positions);
        return map;
    }
}

