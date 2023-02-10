using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShape_Z : BlockShape
{
    public BlockShape_Z(BlockGrid blockGrid) : base(blockGrid) { }

    //public BlockShape_Line(BlockGrid blockGrid)
    public override void CreateBlockShapePositions()
    {
        BlockShapePositions = new BlockShapePosition[][]
        {
            new BlockShapePosition[]
            {
                new BlockShapePosition(1, 1),
                new BlockShapePosition(0, 1),
                new BlockShapePosition(2, 2),
                new BlockShapePosition(1, 2)
            },
            new BlockShapePosition[]
            {
                new BlockShapePosition(0, 2),
                new BlockShapePosition(1, 1),
                new BlockShapePosition(0, 3),
                new BlockShapePosition(1, 2)
            },
            new BlockShapePosition[]
            {
                new BlockShapePosition(1, 1),
                new BlockShapePosition(0, 1),
                new BlockShapePosition(2, 2),
                new BlockShapePosition(1, 2)
            },
            new BlockShapePosition[]
            {
                new BlockShapePosition(0, 2),
                new BlockShapePosition(1, 1),
                new BlockShapePosition(0, 3),
                new BlockShapePosition(1, 2)
            }
        };
    }


    public override int GetPivotBlock()
    {
        return 0;
    }
}
