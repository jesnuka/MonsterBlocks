using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShapeFactory
{
    public BlockShape ShapeLine()
    {
        return new BlockShape_Line();
    }
}
