using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : IBlockGenerator
{


    private IBlockGenerator[] _generators;


    public RandomGenerator( IBlockGenerator[] generators)
    {

        _generators = generators;
    }
    public MapBlock Generate(Vector3Int origin)
    {
        IBlockGenerator generator = _generators[Random.Range(0, _generators.Length)];

        MapBlock map = generator.Generate(origin);
        
        return map;
    }
}
