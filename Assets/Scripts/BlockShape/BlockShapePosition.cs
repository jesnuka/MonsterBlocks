using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShapePosition 
{
    // Block coordinates currently range
    // from top left X:0 Y:0, to bottom right X:4 Y:4

    public int _x;
    public int X { get { return _x; }}

    public int _y;
    public int Y { get { return _y; }}

    public BlockShapePosition(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(X, Y);
    }
}
