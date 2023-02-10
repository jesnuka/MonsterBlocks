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
    public BlockShape ShapeTriangle()
    {
        BlockShape_Triangle blockShape = new BlockShape_Triangle(BlockGrid);
        return blockShape;
    }
    public BlockShape ShapeZ()
    {
        BlockShape_Z blockShape = new BlockShape_Z(BlockGrid);
        return blockShape;
    }
    public BlockShape ShapeS()
    {
        BlockShape_S blockShape = new BlockShape_S(BlockGrid);
        return blockShape;
    }
    public BlockShape ShapeSquare()
    {
        BlockShape_Square blockShape = new BlockShape_Square(BlockGrid);
        return blockShape;
    }
    public BlockShape ShapeL()
    {
        BlockShape_L blockShape = new BlockShape_L(BlockGrid);
        return blockShape;
    }
    public BlockShape ShapeRandom()
    {
        // Choose the type of BlockShape

        int rand = Random.Range(0, 6);

        switch(rand)
        {
            case 0:
                {
                    return ShapeLine();
                }
            case 1:
                {
                    return ShapeTriangle();
                }
            case 2:
                {
                    return ShapeZ();
                }
            case 3:
                {
                    return ShapeS();
                }
            case 4:
                {
                    return ShapeSquare();
                }
            case 5:
                {
                    return ShapeL();
                }
            default:
                {
                    return ShapeLine();
                }
        }
    }
}
