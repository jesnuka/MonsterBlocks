using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShape_Square : BlockShape
{
    public BlockShape_Square(BlockGrid blockGrid) : base(blockGrid) { }

    //public BlockShape_Line(BlockGrid blockGrid)
    public override void CreateBlockShapePositions()
    {
        BlockShapePositions = new BlockShapePosition[][]
        {
            new BlockShapePosition[]
            {
                new BlockShapePosition(1, 1),
                new BlockShapePosition(1, 2),
                new BlockShapePosition(2, 1),
                new BlockShapePosition(2, 2)
            },
            new BlockShapePosition[]
            {
                new BlockShapePosition(1, 1),
                new BlockShapePosition(1, 2),
                new BlockShapePosition(2, 1),
                new BlockShapePosition(2, 2)
            },
            new BlockShapePosition[]
            {
                new BlockShapePosition(1, 1),
                new BlockShapePosition(1, 2),
                new BlockShapePosition(2, 1),
                new BlockShapePosition(2, 2)
            },
            new BlockShapePosition[]
            {
                new BlockShapePosition(1, 1),
                new BlockShapePosition(1, 2),
                new BlockShapePosition(2, 1),
                new BlockShapePosition(2, 2)
            }
        };
    }


    public override int GetPivotBlock()
    {
        return 0;
    }
}
