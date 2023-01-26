using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFactory : MonoBehaviour
{
    BlockGrid _blockGrid;

    [Header("Prefabs")]
    [SerializeField] private GameObject _blockPrefab;

    public BlockFactory(BlockGrid blockGrid)
    {
        _blockGrid = blockGrid;
    }
}
