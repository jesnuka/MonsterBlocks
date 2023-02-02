using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLineChecker
{

    public static event Action<int> OnLinesChecked;
    public static event Action OnLineCheckStarted;

    private BlockGrid _blockGrid;
    public BlockGrid BlockGrid { get { return _blockGrid; } set { _blockGrid = value; } }

    private bool _lineCheckingStarted;
    public bool LineCheckingStarted { get { return _lineCheckingStarted; } set { _lineCheckingStarted = value; } }
    
    public BlockLineChecker(BlockGrid blockGrid)
    {
        BlockGrid = blockGrid;
    }

    public void StartLineChecking(BlockShape blockShape)
    {
        LineCheckingStarted = true;
        OnLineCheckStarted?.Invoke();

        List<Block> blocksToClear = new List<Block>();
        List<int> rowsChecked = new List<int>();

        // Check each row of blockShape and check if blocks are enabled up to ColumnAmount
        // Start checking neighbors from the selectedTile
        foreach(BlockPosition position in blockShape.BlockPositions)
        {
            if (rowsChecked.Contains(position.Row))
                continue;

            rowsChecked.Add(position.Row);
            List<Block> blockLine = new List<Block>();

            blockLine.Add(BlockGrid.BlockColumns[position.Column].Blocks[position.Row]);
            blockLine.AddRange(GetConnectingNeighbor(position, 1));
            blockLine.AddRange(GetConnectingNeighbor(position, -1));

            if (blockLine.Count == BlockGrid.ColumnAmount)
                blocksToClear.AddRange(blockLine);
        }

        // Continue to disabling blocks and adding score, and dropping blocks down
        // (Add new state for it?)
        //   DisableBlocks(blocksToClear);

        OnLinesChecked?.Invoke(blocksToClear.Count);

        LineCheckingStarted = false;
    }

    private List<Block> GetConnectingNeighbor(BlockPosition blockPosition, int direction)
    {
        // Blocks to return if the whole line is connected
        List<Block> blocksToClear = new List<Block>();

        direction = Mathf.Clamp(direction, -1, 1);
      //  if (blockPosition.Row + direction <= (BlockGrid.RowAmount - 1) && blockPosition.Row + direction >= 0)
        if (blockPosition.Column + direction <= (BlockGrid.ColumnAmount - 1) && blockPosition.Column + direction >= 0)
        {
         //   Block neighborBlock = BlockGrid.BlockColumns[blockPosition.Column].Blocks[blockPosition.Row + direction];
            Block neighborBlock = BlockGrid.BlockColumns[blockPosition.Column + direction].Blocks[blockPosition.Row];
            if (neighborBlock != null && neighborBlock.IsEnabled)
            {
                blocksToClear.Add(neighborBlock);
                blocksToClear.AddRange(GetConnectingNeighbor(neighborBlock.BlockPosition, direction));
            }
        }

        return blocksToClear;
    }

}
