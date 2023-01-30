using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShapeFactory
{
    BlockGrid _blockGrid;
    public BlockGrid BlockGrid { get { return _blockGrid; } }

    public BlockShapeFactory(BlockGrid blockGrid)
    {
        _blockGrid = blockGrid;
    }

    public BlockShape CreateBlockShape()
    {
        BlockShape blockShape = ShapeRandom();

        return blockShape;
    }

    public BlockShape ShapeLine()
    {
        BlockShape_Line blockShape = new BlockShape_Line(BlockGrid);
        return blockShape;
    }

    public BlockShape ShapeRandom()
    {
        // Choose the type of BlockShape

        int rand = Random.Range(0, 1);

        switch(rand)
        {
            case 0:
                {
                    return ShapeLine();
                }
            default:
                {
                    return ShapeLine();
                }
        }
    }
}
