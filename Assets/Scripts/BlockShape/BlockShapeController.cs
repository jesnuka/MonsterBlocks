using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShapeController
{

    private BlockGrid _blockGrid;
    public BlockGrid BlockGrid { get { return _blockGrid; } set { _blockGrid = value; } }

    private BlockFactory _blockFactory;
    public BlockFactory BlockFactory { get { return _blockFactory; } set { _blockFactory = value; } }

    private BlockShapeFactory _blockShapeFactory;
    public BlockShapeFactory BlockShapeFactory { get { return _blockShapeFactory; } set { _blockShapeFactory = value; } }

    private BlockShape _currentBlockShape;
    public BlockShape CurrentBlockShape { get { return _currentBlockShape; } set { _currentBlockShape = value; } }

    private BlockShape _previousBlockShape;
    public BlockShape PreviousBlockShape { get { return _previousBlockShape; } set { _previousBlockShape = value; } }

    public static event Action OnBlockShapeCreated;
    public static event Action OnBlockShapePlaced;

    private bool _blockShapeCreated;
    public bool BlockShapeCreated { get { return _blockShapeCreated; } set { _blockShapeCreated = value; } }

    public BlockShapeController(BlockGrid blockGrid, BlockFactory blockFactory)
    {
        BlockGrid = blockGrid;
        BlockFactory = blockFactory;
        CreateBlockShapeFactory();
    }
    private void CreateBlockShapeFactory()
    {
        BlockShapeFactory = new BlockShapeFactory(BlockGrid);
    }

    public void CreateNewShape()
    {
        // Create new shape of blocks to be moved down
        if (BlockShapeFactory == null)
            CreateBlockShapeFactory();

        CurrentBlockShape = BlockShapeFactory.CreateBlockShape();

        MoveShape(0,0);

        BlockShapeCreated = true;
        OnBlockShapeCreated?.Invoke();
    }

    public void ToggleShapeBlocks(BlockShape blockShape, bool value)
    {
        for (int i = 0; i < blockShape.BlockPositions.Length; i++)
        {
            int column = blockShape.BlockPositions[i].Column;
            int row = blockShape.BlockPositions[i].Row;

            Block block = BlockGrid.BlockColumns[column].Blocks[row];
            block.ToggleBlock(value);
        }
    }
    public void PlaceShape()
    {
        // TODO:
        // Add locking of block to place

        OnBlockShapePlaced?.Invoke();
    }

    private bool CheckSpawnCollision(BlockShape blockShape)
    {
        // Called when spawning BlockShapes
        // Check if blocks already exist in BlockShape positions
        // if they do, game ends

        // TODO: Not necessary, call CheckCollision

        return true;
    }

    private bool CheckCollision(BlockPosition blockPosition)
    {
        // Check if block already exist in the BlockShape position, or the position is out of bounds

        if (BlockGrid.GetBlock(blockPosition.Column, blockPosition.Row).IsEnabled ||
            blockPosition.Column >= BlockGrid.ColumnAmount ||
            blockPosition.Row >= BlockGrid.RowAmount ||
            blockPosition.Row < 0 ||
            blockPosition.Column < 0)
            return false;
        else
            return true;
    }

    private void CheckStopCollision(BlockShape blockShape)
    {
        // Called each time BlockShape is moved down
        // Check if blocks already exist in BlockShape positions
        // If they do, lock the BlockShape in place

        // TODO: Not necessary, remove and call CheckCollision + PlaceShape from elsewhere
    }

    #region Movement & Rotation
    public void MoveShapeDown()
    {
        MoveShape(0, -1);
    }

    public void MoveShapeLeft()
    {
        MoveShape(-1, 0);
    }

    public void MoveShapeRight()
    {
        MoveShape(1, 0);

    }

    private void MoveShape(int xDirection, int yDirection)
    {
        // Store previous shape
        if (CurrentBlockShape != null && BlockShapeCreated)
        {
            PreviousBlockShape = CurrentBlockShape;
            //Disable previous shape
            ToggleShapeBlocks(PreviousBlockShape, false);
        }

        BlockPosition[] newPositions = CurrentBlockShape.GetMovedBlockPositions(xDirection, yDirection);

        if (newPositions == null)
        {
            // Toggle back previous shape
            ToggleShapeBlocks(PreviousBlockShape, true);
            return;
        }

        bool canMove = true;
        foreach(BlockPosition blockPosition in newPositions)
            canMove = CheckCollision(blockPosition);

        if (!canMove)
        {
            // Toggle back previous shape
            ToggleShapeBlocks(PreviousBlockShape, true);
            return;
        }


        CurrentBlockShape.BlockPositions = newPositions;
        Debug.Log("Removing old");

        Debug.Log("adding new");
        // Enable the blocks next
        ToggleShapeBlocks(CurrentBlockShape, true);
    }

    public void RotateShapeClockwise()
    {
        RotateShape(1);
    }
    public void RotateShapeCounterclockwise()
    {
        RotateShape(-1);
    }

    private void RotateShape(int rotationDirection)
    {
        BlockPosition[] newPositions = CurrentBlockShape.GetRotatedBlockPositions(rotationDirection);

        if (newPositions == null)
            return;

        bool canRotate = true;
        foreach (BlockPosition blockPosition in newPositions)
            canRotate = CheckCollision(blockPosition);

        if (!canRotate)
            return;

        // TODO:
        // Call Rotate method of the currentBlockShape,
        // check for collision of new positions,
        // disable old shape and enable new shape
    }

    #endregion

}
