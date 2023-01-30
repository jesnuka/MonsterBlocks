using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockShape
{
    BlockGrid _blockGrid;
    public BlockGrid BlockGrid { get { return _blockGrid; } set { _blockGrid = value; } }

    public int BlockAmount { get { if (BlockShapePositions[0] != null) return BlockShapePositions[0].Length; else return 0; } }

    // Current rotation of the shape, determines which blockPositions to use
    private int _currentRotation;
    public int CurrentRotation { get { return _currentRotation; } set { _currentRotation = value % 4; } }

    // Array of BlockShapePosition arrays, which contain coordinates where to place all the blocks that create this BlockShape
    // First array = block rotations, second array = amount of blocks
    // Used for creating the shape
    private BlockShapePosition[][] _blockShapePositions;
    public BlockShapePosition[][] BlockShapePositions { get { return _blockShapePositions; } set { _blockShapePositions = value; } }

    // Array of BlockPositions, for each block in the BlockShape
    // Used for moving and rotating the Blocks within the BlockShape
    private List<BlockPosition> _blockPositions;
    public List<BlockPosition> BlockPositions { get { return _blockPositions; } set { _blockPositions = value; } }
    public void RotateClockwise()
    {
        CurrentRotation += 1;
    }
    public void RotateCounterclockwise()
    {
        CurrentRotation -= 1;
    }

    public BlockShape(BlockGrid blockGrid)
    {
        BlockGrid = blockGrid;
        Debug.Log("Constructed a BLOCKSHAPE");
        CreateBlockShapePositions();
        CreateBlockShape(BlockGrid.SpawnPosition);
    }

    public abstract void CreateBlockShapePositions();

    private BlockPosition GetBlockPosition(BlockPosition spawnPosition, BlockShapePosition blockShapePosition)
    {
        // Calculate the position of the block based on the BlockShapePosition
        int column = spawnPosition.Column + blockShapePosition.X; 
        int row = spawnPosition.Row + blockShapePosition.Y;

        BlockPosition blockPosition = new BlockPosition(column, row);

        return blockPosition;
    }

    public void CreateBlockShape(BlockPosition spawnPosition)
    {
        if (BlockAmount <= 0)
            return;

        BlockPositions = new List<BlockPosition>();

        for (int i = 0; i < BlockAmount; i++)
            BlockPositions.Add(GetBlockPosition(spawnPosition, BlockShapePositions[CurrentRotation][i]));



    }

    public BlockShapePosition[] ReturnCurrentBlocks()
    {
        if (BlockShapePositions[CurrentRotation] == null)
            return null;

        return BlockShapePositions[CurrentRotation];
    }

}
