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
    // Blocks within the BlockShape are handled as separate blocks, except when rotating
    private BlockPosition[] _blockPositions;
    public BlockPosition[] BlockPositions { get { return _blockPositions; } set { _blockPositions = value; } }

    public void RotateShape(int rotationDirection)
    {
        CurrentRotation += rotationDirection;
    }

    public BlockShape(BlockGrid blockGrid)
    {
        BlockGrid = blockGrid;
        CreateBlockShapePositions();
        CreateBlockShape(BlockGrid.SpawnPosition);
    }

    public abstract void CreateBlockShapePositions();

    // Returns the index of the block of the specific shape that used as the pivot for rotating the shape
    public abstract int GetPivotBlock();

    private BlockPosition GetBlockPosition(BlockPosition blockPosition, BlockShapePosition blockShapePosition)
    {
        // Calculate the position of the block based on the BlockShapePosition
        int column = blockPosition.Column + blockShapePosition.X; 
        int row = blockPosition.Row + blockShapePosition.Y;

        if (column >= BlockGrid.ColumnAmount || row >= BlockGrid.RowAmount)
            return null;

        return new BlockPosition(column, row);
    }
    public BlockPosition[] GetRotatedBlockPositions(int rotationDirection)
    {
        // Get new direction
        int newDirection = ((CurrentRotation + BlockShapePositions.Length) - rotationDirection) % BlockShapePositions.Length;

        // Get the shape of the blocks in the new direction
        BlockShapePosition[] newShapePosition = BlockShapePositions[newDirection];

        BlockPosition[] rotatedBlockPositions = new BlockPosition[BlockPositions.Length];

        int pivotIndex = GetPivotBlock();

        // Get BlockPosition of the rotated blocks based on the pivot Block index
        for (int i = 0; i < rotatedBlockPositions.Length; i++)
        {
            if(i == pivotIndex)
            {
                // Keep the same position for the pivot Block to anchor the shape
                rotatedBlockPositions[i] = BlockPositions[i];
            }

            else
            {
                // For other blocks, calculate the new position based on the pivot Block

                int column = BlockPositions[pivotIndex].Column + (BlockShapePositions[newDirection][pivotIndex].X - BlockShapePositions[newDirection][i].X);
                int row = BlockPositions[pivotIndex].Row + (BlockShapePositions[newDirection][pivotIndex].Y - BlockShapePositions[newDirection][i].Y);

                if (column >= BlockGrid.ColumnAmount || column < 0 || row >= BlockGrid.RowAmount || row < 0)
                    return null;

                rotatedBlockPositions[i] = new BlockPosition(column, row);
            }
        }

        return rotatedBlockPositions;
    }
    public BlockPosition[] GetMovedBlockPositions(int xDirection, int yDirection)
    {
        BlockPosition[] movedBlockPositions = new BlockPosition[BlockPositions.Length];
        for(int i = 0; i < BlockPositions.Length; i++)
        {
            int column = BlockPositions[i].Column + xDirection;
            int row = BlockPositions[i].Row + yDirection;

            if (column >= BlockGrid.ColumnAmount || column < 0 || row >= BlockGrid.RowAmount || row < 0)
                return null;

            movedBlockPositions[i] = new BlockPosition(column, row);
        }

        return movedBlockPositions;
    }

    public void CreateBlockShape(BlockPosition spawnPosition)
    {
        if (BlockAmount <= 0)
            return;

        BlockPositions = new BlockPosition[BlockAmount];

        for (int i = 0; i < BlockAmount; i++)
            BlockPositions[i] = GetBlockPosition(spawnPosition, BlockShapePositions[CurrentRotation][i]);
    }

    public BlockShapePosition[] GetCurrentBlockShapePosition()
    {
        if (BlockShapePositions[CurrentRotation] == null)
            return null;

        return BlockShapePositions[CurrentRotation];
    }

}
