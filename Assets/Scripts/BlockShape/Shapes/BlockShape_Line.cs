using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShape_Line : BlockShape
{
    public BlockShape_Line(BlockGrid blockGrid) : base(blockGrid) { }

    //public BlockShape_Line(BlockGrid blockGrid)
    public override void CreateBlockShapePositions()
    {
        BlockShapePositions = new BlockShapePosition[][]
        {
            new BlockShapePosition[]
            {
                new BlockShapePosition(0, 1),
                new BlockShapePosition(1, 1),
                new BlockShapePosition(2, 1),
                new BlockShapePosition(3, 1),
            },
            new BlockShapePosition[]
            {
                new BlockShapePosition(2, 0),
                new BlockShapePosition(2, 1),
                new BlockShapePosition(2, 2),
                new BlockShapePosition(2, 3),
            },
            new BlockShapePosition[]
            {
                new BlockShapePosition(0, 2),
                new BlockShapePosition(1, 2),
                new BlockShapePosition(2, 2),
                new BlockShapePosition(3, 2),
            },
            new BlockShapePosition[]
            {
                new BlockShapePosition(1, 0),
                new BlockShapePosition(1, 1),
                new BlockShapePosition(1, 2),
                new BlockShapePosition(1, 3),
            },
        };
    }


    public override int GetPivotBlock()
    {
        return 0;
    }
}
