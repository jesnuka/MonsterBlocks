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
    public static event Action OnCanNotPlaceShape;

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
        // BUGI, TULEE NULL KUN TULEE MENUSTA TAKAISIN PELIIN
        //
        // (JA SHAPE SIJAINTI EI RESETOIDU MYÖSKÄÄN, RESETTAA GRIDI KUNNOLLA)


        if (BlockShapeFactory == null)
            CreateBlockShapeFactory();

        // Create new shape of blocks to be moved down
        CurrentBlockShape = BlockShapeFactory.CreateBlockShape();

        bool moveResult = MoveShape(0,0);

        if(moveResult)
        {
            BlockShapeCreated = true;
            OnBlockShapeCreated?.Invoke();
        }
        else
        {
            // Can not spawn a new shape, therefore game is lost
            OnCanNotPlaceShape?.Invoke();
        }

    }

    public void ResetController()
    {
        BlockShapeCreated = false;
        BlockMoveStarted = false;
        CurrentBlockShape = null;
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
        if (CurrentBlockShape == null)
            return false;

        // Store and disable previous shape
        StorePreviousShape();

        BlockPosition[] newPositions = CurrentBlockShape.GetMovedBlockPositions(xDirection, yDirection);

        if (newPositions == null || !BlockGrid.CheckCollision(newPositions))
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

    private void StorePreviousShape()
    {
        // Store and disable previous shape
        if (CurrentBlockShape != null && BlockShapeCreated)
        {
            PreviousBlockShape = CurrentBlockShape;
            //Disable previous shape
            ToggleShapeBlocks(PreviousBlockShape, false);
        }
    }

    private void RotateShape(int rotationDirection)
    {
        // Store and disable previous shape
        StorePreviousShape();

        BlockPosition[] newPositions = CurrentBlockShape.GetRotatedBlockPositions(rotationDirection);

        if (newPositions == null || !BlockGrid.CheckCollision(newPositions))
        {
            // Toggle back previous shape
            ToggleShapeBlocks(PreviousBlockShape, true);
            return;
        }

        CurrentBlockShape.RotateShape(rotationDirection);
        CurrentBlockShape.BlockPositions = newPositions;

        // Enable the blocks next
        ToggleShapeBlocks(CurrentBlockShape, true);
    }

    #endregion

}
