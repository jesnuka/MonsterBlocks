using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShapeController : MonoBehaviour
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

    // Used to check when to initiate a new block MoveDown sequence
    private bool _blockMoveStarted;
    public bool BlockMoveStarted { get { return _blockMoveStarted; } set { _blockMoveStarted = value; } }

    private bool _blockShapePlaced;
    public bool BlockShapePlaced { get { return _blockShapePlaced; } set { _blockShapePlaced = value; } }

    public void SetupController(BlockGrid blockGrid, BlockFactory blockFactory)
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
        BlockShapeCreated = false;
        OnBlockShapePlaced?.Invoke();
    }

    public void StartBlockTimedMovement()
    {
        // Start block moving down by BlockSpeed time
        if(!BlockMoveStarted)
            StartCoroutine(DropBlock());
    }

    private IEnumerator DropBlock()
    {
        BlockMoveStarted = true;
        yield return new WaitForSeconds(BlockGrid.GameSettings.BlockSpeed);
        if (!BlockShapePlaced)
            MoveShapeDown();
        BlockMoveStarted = false;

    }

    private bool CheckCollision(BlockPosition[] newPositions)
    {
        // Check if block already exist in the BlockShape position, or the position is out of bounds

        foreach (BlockPosition blockPosition in newPositions)
            if (BlockGrid.GetBlock(blockPosition.Column, blockPosition.Row).IsEnabled ||
                blockPosition.Column >= BlockGrid.ColumnAmount ||
                blockPosition.Row >= BlockGrid.RowAmount ||
                blockPosition.Row < 0 ||
                blockPosition.Column < 0)
                return false;

        return true;
    }

    #region Movement & Rotation
    public void MoveShapeDown()
    {
        if(!MoveShape(0, -1))
            PlaceShape();
    }

    public void MoveShapeLeft()
    {
        MoveShape(-1, 0);
    }

    public void MoveShapeRight()
    {
        MoveShape(1, 0);

    }

    private bool MoveShape(int xDirection, int yDirection)
    {
        // Store previous shape
        if (CurrentBlockShape != null && BlockShapeCreated)
        {
            PreviousBlockShape = CurrentBlockShape;
            //Disable previous shape
            ToggleShapeBlocks(PreviousBlockShape, false);
        }

        BlockPosition[] newPositions = CurrentBlockShape.GetMovedBlockPositions(xDirection, yDirection);

        if (newPositions == null || !CheckCollision(newPositions))
        {
            // Toggle back previous shape
            ToggleShapeBlocks(PreviousBlockShape, true);
            return false;
        }

        CurrentBlockShape.BlockPositions = newPositions;

        // Enable the blocks next
        ToggleShapeBlocks(CurrentBlockShape, true);

        return true;
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
        // TODO: Combine with MoveShape to remove duplicate code

        // Store previous shape
        if (CurrentBlockShape != null && BlockShapeCreated)
        {
            PreviousBlockShape = CurrentBlockShape;
            //Disable previous shape
            ToggleShapeBlocks(PreviousBlockShape, false);
        }

        BlockPosition[] newPositions = CurrentBlockShape.GetRotatedBlockPositions(rotationDirection);

        if (newPositions == null || !CheckCollision(newPositions))
        {
            // Toggle back previous shape
            ToggleShapeBlocks(PreviousBlockShape, true);
            return;
        }

        // TODO:
        // Call Rotate method of the currentBlockShape,
        // check for collision of new positions,
        // disable old shape and enable new shape
    }

    #endregion

}
