using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockColumn : MonoBehaviour
{
    public Block[] _blocks;
    public Block[] Blocks { get { return _blocks; } set { _blocks = value; } }
}

