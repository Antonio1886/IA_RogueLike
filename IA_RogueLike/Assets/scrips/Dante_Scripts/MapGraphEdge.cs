using UnityEngine;

public class MapGraphEdge<T>
{
    public MapGraphNode<T> target;
    public Vector3Int offset;

    public MapGraphEdge(MapGraphNode<T> target, Vector3Int offset)
    {
        this.target = target;
        this.offset = offset;
    }
}
