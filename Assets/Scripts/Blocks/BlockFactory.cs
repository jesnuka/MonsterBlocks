using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFactory : MonoBehaviour
{
    BlockGrid _gameGrid;
    public BlockGrid GameGrid { get { return _gameGrid; } }

    [Header("Prefabs")]
    [SerializeField] private GameObject _columnPrefab;
    public GameObject ColumnPrefab { get { return _columnPrefab; } }

    [SerializeField] private GameObject _blockPrefab;
    public GameObject BlockPrefab { get { return _blockPrefab; }}

    public BlockFactory(BlockGrid blockGrid)
    {
        _gameGrid = blockGrid;
    }
    public Block CreateBlock(Vector3 position, Transform parent, BlockGrid blockGrid)
    {
        Debug.Log("Position is: " + position);
        GameObject blockObject = Instantiate(BlockPrefab, position, Quaternion.identity);
        blockObject.transform.SetParent(parent, false);
        Block block = blockObject.GetComponent<Block>();
        block.BlockSprite.DeactivateBlock();
        block.GameGrid = GameGrid;

        return block;
    }
    public BlockColumn CreateColumn(int rowAmount, Vector3 position, Transform parent)
    {
        GameObject blockColumnObject = Instantiate(ColumnPrefab, position, Quaternion.identity);
        blockColumnObject.transform.SetParent(parent, false);
        BlockColumn blockColumn = blockColumnObject.GetComponent<BlockColumn>();

        blockColumn.Blocks = new Block[rowAmount];

        return blockColumn;
    }

    public Vector2 GetBlockPrefabSize()
    {
        return BlockPrefab.GetComponent<Block>().GetSize();
    }
}
