using UnityEngine;

public interface IBlockGenerator
{
    MapBlock Generate(Vector3Int origin);
}
