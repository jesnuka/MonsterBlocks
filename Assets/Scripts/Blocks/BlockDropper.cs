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

    private int _linesCleared;
    public int LinesCleared { get { return _linesCleared; } set { _linesCleared = value; } }

    private int _lineMovesLeft;
    public int LineMovesLeft { get { return _lineMovesLeft; } set { _lineMovesLeft = value; } }

    private int _lowestRowToCheck;
    public int LowestRowToCheck { get { return _lowestRowToCheck; } set { _lowestRowToCheck = value; } }

    public void Initialize(BlockGrid blockGrid)
    {
        BlockGrid = blockGrid;
    }

    public void PerformBlockMovement()
    {
        // Blocks have to be moved down by the amount of lines that were removed.
        // This is only done to blocks above the cleared lines and it is done
        // incrementally with a small delay between to show it visually to the player

        if(LineMovesLeft > 0)
        {
            // Get blocks to be moved down

            List<Block> blocksToMove = new List<Block>();
            List<Block> allBlocks = BlockGrid.ReturnEnabledBlocks();

            if (allBlocks.Count > 0)
                foreach (Block block in allBlocks)
                    if (block.BlockPosition.Row >= LowestRowToCheck)
                        blocksToMove.Add(block);
            else
                LineMovesLeft = 0;

            // Move blocks until lineMovesLeft == 0

            List<Block> newBlocks = new List<Block>();

            foreach (Block block in blocksToMove)
            {
                Block newPosition = GetNewBlockPosition(block);
                if (newPosition != null)
                    newBlocks.Add(newPosition);
            }

            StartCoroutine(BlockMoveDelay(blocksToMove, newBlocks));

        }
        else
            OnNothingMoved?.Invoke();


    }

    IEnumerator BlockMoveDelay(List<Block> blocksToMove, List<Block> newBlocks)
    {
        // Move blocks after delay
        yield return new WaitForSeconds(0.1f);
        MoveBlocks(blocksToMove, newBlocks);
    }

    private Block GetNewBlockPosition(Block block)
    {
        BlockPosition newPosition = BlockGrid.GetMovedBlockPosition(block, 0, -1);
        if (newPosition != null)
            return BlockGrid.GetBlock(newPosition.Column, newPosition.Row);
        else
            return null;
    }

    private void MoveBlocks(List<Block> blocksToMove, List<Block> newBlocks)
    {
        foreach(Block block in blocksToMove)
            if(block != null)
                block.ToggleBlock(false);
        foreach (Block block in newBlocks)
            if (block != null)
                block.ToggleBlock(true);

        LineMovesLeft -= 1;
        OnBlocksMoved?.Invoke();
    }
}
