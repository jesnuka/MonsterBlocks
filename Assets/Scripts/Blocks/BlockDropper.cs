using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDropper : MonoBehaviour 
{
    public static event Action OnNothingMoved;
    public static event Action OnBlocksMoved;

    private BlockGrid _blockGrid;
    public BlockGrid BlockGrid { get { return _blockGrid; } set { _blockGrid = value; } }

    public void Initialize(BlockGrid blockGrid)
    {
        BlockGrid = blockGrid;
    }

    public void CheckMoveBlocks()
    {
        // Go through grid, check for blocks that are active, and if they have an empty space below them
        // If nothing has, invoke below, otherwise MoveBlocks;
        bool blocksMoved = false;
        List<Block> enabledBlocks = BlockGrid.ReturnEnabledBlocks();
        List<Block> blocksToDisable = new List<Block>();
        List<Block> blocksToEnable = new List<Block>();
        if(enabledBlocks.Count > 0)
        {
            foreach(Block block in enabledBlocks)
            {
                Block newPosition = CheckBlockNewPosition(block);
                if(newPosition != null)
                {
                    blocksMoved = true;
                    blocksToDisable.Add(block);
                    blocksToEnable.Add(newPosition);
                }
            }
        }

        if(!blocksMoved)
            OnNothingMoved?.Invoke();
        else
            StartCoroutine(BlockMoveDelay(blocksToDisable, blocksToEnable));


    }

    IEnumerator BlockMoveDelay(List<Block> blocksToDisable, List<Block> blocksToEnable)
    {
        // Move blocks after delay
        yield return new WaitForSeconds(0.1f);
        MoveBlocks(blocksToDisable, blocksToEnable);
    }

    private Block CheckBlockNewPosition(Block block)
    {
        // Check if block can be moved down, if it can be, the new position is returned
        BlockPosition newPosition = BlockGrid.GetMovedBlockPosition(block, 0, -1);
        if (BlockGrid.CheckCollision(newPosition))
        {
            return BlockGrid.GetBlock(newPosition.Column, newPosition.Row);
        }
        else
            return null;
    }

    private void MoveBlocks(List<Block> blocksToDisable, List<Block> blocksToEnable)
    {
        foreach(Block block in blocksToDisable)
            if(block != null)
                block.ToggleBlock(false);
        foreach (Block block in blocksToEnable)
            if (block != null)
                block.ToggleBlock(true);

        OnBlocksMoved?.Invoke();
    }
}
