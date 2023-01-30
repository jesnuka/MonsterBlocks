using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockShape
{
    public int BlockAmount { get { if (BlockPositions[0] != null) return BlockPositions[0].Length; else return 1; } }

    // Current rotation of the shape, determines which blockPositions to use
    private int _currentRotation;
    public int CurrentRotation { get { return _currentRotation; } set { _currentRotation = value % 4; } }

    // Array of BlockShapePosition arrays, which contain coordinates where to place all the blocks that create this BlockShape
    // First array = block rotations, second array = amount of blocks
    private BlockShapePosition[][] _blockPositions;
    public BlockShapePosition[][] BlockPositions { get { return _blockPositions; } set { _blockPositions = value; } }
    public void RotateClockwise()
    {
        CurrentRotation += 1;
    }
    public void RotateCounterclockwise()
    {
        CurrentRotation -= 1;
    }

    public BlockShapePosition[] ReturnCurrentBlocks()
    {
        if (BlockPositions[CurrentRotation] == null)
            return null;

        return BlockPositions[CurrentRotation];
    }

}
