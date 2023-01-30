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


    public static event Action OnBlockShapeCreated;
    public static event Action OnBlockShapePlaced;

    public BlockShapeController(BlockGrid blockGrid)
    {
        BlockGrid = blockGrid;
        CreateBlockFactory();
        CreateBlockShapeFactory();
    }

    private void CreateBlockFactory()
    {
        BlockFactory = new BlockFactory(BlockGrid);
    }
    private void CreateBlockShapeFactory()
    {
        BlockShapeFactory = new BlockShapeFactory(BlockGrid);
    }

    public void CreateNewShape()
    {
        // Create new shape of blocks to be moved down

        if (BlockFactory == null)
            CreateBlockFactory();

        if (BlockShapeFactory == null)
            CreateBlockShapeFactory();

        CurrentBlockShape = BlockShapeFactory.CreateBlockShape();

        // TODO: check here for initial BlockShape collision!
        CheckSpawnCollision(CurrentBlockShape);

        // Enable the blocks next
        // EnableBlocks(CurrentBlockShape);
    }

    private void CheckSpawnCollision(BlockShape blockShape)
    {
        // Called when spawning BlockShapes
        // Check if blocks already exist in BlockShape positions
        // if they do, game ends
    }

    private void CheckCollision(BlockShape blockShape)
    {
        // Called when Moving or Rotating BlockShapes
        // Check if blocks already exist in BlockShape positions
        // If they do, movement and rotation can't be done
        // TODO: Add checks for rotation (Can't be rotated) and movement sideways (Can't be moved)
    }

    private void CheckStopCollision(BlockShape blockShape)
    {
        // Called each time BlockShape is moved down
        // Check if blocks already exist in BlockShape positions
        // If they do, lock the BlockShape in place
    }

    #region Movement & Rotation
    public void MoveShapeDown()
    {

    }

    public void MoveShapeLeft()
    {

    }

    public void MoveShapeRight()
    {

    }
    public void RotateShapeClockwise()
    {

    }
    public void RotateShapeCounterclockwise()
    {

    }

    #endregion

}
