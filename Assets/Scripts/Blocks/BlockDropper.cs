using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDropper
{
    public static event Action OnNothingMoved;
    public static event Action OnBlocksMoved;

    private BlockGrid _blockGrid;
    public BlockGrid BlockGrid { get { return _blockGrid; } set { _blockGrid = value; } }

    public BlockDropper(BlockGrid blockGrid)
    {
        BlockGrid = blockGrid;
    }

    public void CheckMoveBlocks()
    {
        // Go through grid, check for blocks that are active, and if they have an empty space below them
        // If nothing has, invoke below, otherwise MoveBlocks;
        OnNothingMoved?.Invoke();
    }

    private void MoveBlocks()
    {
        OnBlocksMoved?.Invoke();
    }
}
