using UnityEngine;
using UnityEngine.Tilemaps;

public interface ITileFactory
{
    Tile GetTile(Vector3Int position);
    //Da un tile apartir de una lista de tiles
}
