using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFactory : MonoBehaviour
{
    BlockGrid _blockGrid;
    public BlockGrid BlockGrid { get { return _blockGrid; } }

    [Header("Prefabs")]
    [SerializeField] private GameObject _columnPrefab;
    public GameObject ColumnPrefab { get { return _columnPrefab; } }

    [SerializeField] private GameObject _blockPrefab;
    public GameObject BlockPrefab { get { return _blockPrefab; }}

    public BlockFactory(BlockGrid blockGrid)
    {
        _blockGrid = blockGrid;
    }
    public Block CreateBlock(Vector3 position, Transform parent, BlockGrid blockGrid)
    {
      //  Debug.Log("Position is: " + position);
        GameObject blockObject = Instantiate(BlockPrefab, position, Quaternion.identity);
        blockObject.transform.SetParent(parent, false);
        Block block = blockObject.GetComponent<Block>();
        block.BlockSprite.ToggleBlock(false);
        block.BlockGrid = BlockGrid;

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
