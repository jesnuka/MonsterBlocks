using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShape_Line : BlockShape
{
    public BlockShape_Line()
    {
        BlockPositions = new BlockShapePosition[][]
        {
            new BlockShapePosition[]
            {
                new BlockShapePosition(0, 2),
                new BlockShapePosition(1, 2),
                new BlockShapePosition(2, 2),
                new BlockShapePosition(3, 2),
            },
        }; 

    }
}
