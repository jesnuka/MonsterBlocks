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
        if (CurrentBlockShape != null)
        {
            // Store old shape temporarily
            PreviousBlockShape = CurrentBlockShape;
            ToggleShapeBlocks(PreviousBlockShape, false);
        }

        // Create new shape of blocks to be moved down
        if (BlockShapeFactory == null)
            CreateBlockShapeFactory();

        CurrentBlockShape = BlockShapeFactory.CreateBlockShape();

        // TODO: Remove duplicate code from this and MoveShape!

        // TODO: check here for initial BlockShape collision!
        bool canSpawn = CheckSpawnCollision(CurrentBlockShape);

        if(!canSpawn)
        {
            CurrentBlockShape = PreviousBlockShape;
        }

        Debug.Log("disabling old shape");
        //Disable old shape
        //ToggleShapeBlocks(CurrentBlockShape, false);

        Debug.Log("enabling new shape");
        // Enable the blocks next
        ToggleShapeBlocks(CurrentBlockShape, true);

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

    private bool CheckCollision(BlockPosition blockShape)
    {
        // Called when Moving or Rotating BlockShapes
        // Check if blocks already exist in BlockShape positions
        // If they do, movement and rotation can't be done
        // TODO: Add checks for rotation (Can't be rotated) and movement sideways (Can't be moved)

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
        if (CurrentBlockShape != null)
        {
            // Store previous shape temporarily
            PreviousBlockShape = CurrentBlockShape;
            ToggleShapeBlocks(PreviousBlockShape, false);
        }
        BlockPosition[] newPositions = CurrentBlockShape.GetMovedBlockPositions(xDirection, yDirection);

        if (newPositions == null)
            return;

        bool canMove = true;
        foreach(BlockPosition blockPosition in newPositions)
            canMove = CheckCollision(blockPosition);

        if (!canMove)
            return;

        CurrentBlockShape.BlockPositions = newPositions;
        Debug.Log("Old shape posRow: " + PreviousBlockShape.BlockPositions[0].Row);
        Debug.Log("New shape posRow: " + CurrentBlockShape.BlockPositions[0].Row);

        //Disable previous shape
        ToggleShapeBlocks(PreviousBlockShape, false);

        // Enable the blocks next
        ToggleShapeBlocks(CurrentBlockShape, true);

        OnBlockShapeCreated?.Invoke();
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
