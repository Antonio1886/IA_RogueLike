using UnityEngine;
public enum Direction
{
    Center,
    North,
    South,
    East, 
    West,
    Southwest,
    Northeast,
    None
}
public class DirectionalPlacer 
{
    private int _spacing;

    public DirectionalPlacer(int spacing)
    {
        _spacing = spacing;
    }

    public Vector3Int GetAdjacentPosition(MapBlock reference, Direction direction, int newWidth, int newHeight)
    {
        Vector3Int EntryPoint = reference.EntryPoint;
        int refWidth = reference.width;
        int refHeight = reference.height;

        //Debug.Log("EntryPoint: " + EntryPoint);

        switch (direction)
        {
            case Direction.North: 
                return EntryPoint + new Vector3Int(refWidth / 2, refHeight + _spacing,0);
            case Direction.South:
                return EntryPoint + new Vector3Int( refWidth/2, -newHeight - _spacing, 0);
            case Direction.East:
                return EntryPoint + new Vector3Int(refWidth+ _spacing, refHeight/2, 0);
            case Direction.West:
                return EntryPoint + new Vector3Int(-newWidth - _spacing, refHeight / 2, 0);
            case Direction.Southwest:
                return EntryPoint + new Vector3Int( - _spacing, -newHeight, 0);
            case Direction.Northeast:
                return EntryPoint + new Vector3Int(refWidth-newWidth, refHeight + 1, 0);
            case Direction.Center:
                return reference.ExitPoint;
            case Direction.None:
                return Vector3Int.zero;
            default: return Vector3Int.zero;
        }

    }
    public Vector3Int GetAdjacentPosition( Direction direction, int distance)
    {
        _spacing = distance;
        //Debug.Log("EntryPoint: " + EntryPoint);

        switch (direction)
        {
            case Direction.North:
                return   new Vector3Int(0 / 2, 0 + _spacing, 0);
            case Direction.South:
                return  new Vector3Int(0 / 2, -0 - _spacing, 0);
            case Direction.East:
                return new Vector3Int(0 + _spacing, 0 / 2, 0);
            case Direction.West:
                return new Vector3Int(-0 - _spacing, 0 / 2, 0);
            case Direction.Southwest:
                return new Vector3Int(-_spacing, -0, 0);
            case Direction.Northeast:
                return new Vector3Int(0 - 0, 0 + 1, 0);
            case Direction.Center:
                return Vector3Int.zero;
            case Direction.None:
                return Vector3Int.zero;
            default: return Vector3Int.zero;
        }

    }
}

