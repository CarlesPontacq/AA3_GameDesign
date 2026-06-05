using UnityEngine;

public static class DirectionUtils
{
    public enum Direction { Left, Right, Up, Down, None }

    public static Vector2 ToVector2(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left: return Vector2.left;
            case Direction.Right: return Vector2.right;
            case Direction.Up: return Vector2.up;
            case Direction.Down: return Vector2.down;
            default: return Vector2.zero;
        }
    }
}