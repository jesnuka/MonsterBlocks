using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockColumn : MonoBehaviour
{
    public Block[] _blocks;
    public Block[] Blocks { get { return _blocks; } set { _blocks = value; } }

    ~BlockColumn()
    {
        foreach (Block block in Blocks)
                Destroy(block.gameObject);
    }
}

