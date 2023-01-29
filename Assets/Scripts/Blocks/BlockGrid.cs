using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrid : MonoBehaviour
{
    public static event Action OnBlocksInitialized;


    [Header("Managers")]
    [SerializeField] private GameStateManager _gameStateManager;
    public GameStateManager GameStateManager { get { return _gameStateManager; } }

    [SerializeField] private GameSettings _gameSettings;
    public GameSettings GameSettings { get { return _gameSettings; } }

    [SerializeField] private BlockFactory _blockFactory;
    public BlockFactory BlockFactory { get { return _blockFactory; } }

    [Header("UI Elements")]
    [Tooltip("Contains the block columns, which contain blocks")]
    [SerializeField] private GameObject _blockGridObject;
    public GameObject BlockGridObject { get { return _blockGridObject; } }

    [SerializeField] private GameObject _blockGridBackground;
    public GameObject BlockGridBackground { get { return _blockGridBackground; } }

    private BlockColumn[] _blockColumns;
    public BlockColumn[] BlockColumns { get { return _blockColumns; } set { _blockColumns = value; } }

    private int _columnAmount;
    public int ColumnAmount { get { return _columnAmount; } set { _columnAmount = value; } }

    private int _rowAmount;
    public int RowAmount { get { return _rowAmount; } set { _rowAmount = value; } }

    public void SetupGrid()
    {
        ColumnAmount = Mathf.Clamp(GameSettings.CurrentColumnAmount, 1, GameSettings.MaxColumnAmount);
        RowAmount = Mathf.Clamp(GameSettings.CurrentRowAmount, 1, GameSettings.MaxRowAmount);

        Debug.Log("Size y: " + BlockGridObject.GetComponent<RectTransform>().sizeDelta.y);
        // Align game board according to the size
        BlockGridObject.GetComponent<RectTransform>().offsetMax = new Vector2(
        BlockGridObject.GetComponent<RectTransform>().offsetMax.x,
        BlockGridObject.GetComponent<RectTransform>().offsetMax.y - ((RowAmount - 1) * BlockFactory.GetBlockPrefabSize().y));

        // Aligh background 
        int odd = ColumnAmount % 2;
        BlockGridBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(
            (ColumnAmount) * BlockFactory.GetBlockPrefabSize().x - (odd * BlockFactory.GetBlockPrefabSize().x),
            BlockGridBackground.GetComponent<RectTransform>().sizeDelta.y);

    }

    public void ResetBoard()
    {

    }

    public void CreateBlockGrid()
    {
        SetupGrid();
        CreateBlocks();

        OnBlocksInitialized?.Invoke();
    }

    private void CreateBlocks()
    {
        // Create each column and occupy them with empty blocks up to row amount
        BlockColumns = new BlockColumn[ColumnAmount];

        for (int x = 0; x < ColumnAmount; x++)
        {
            // Create a column that contains empty blocks
            Vector3 columnPos = new Vector3(x * BlockFactory.GetBlockPrefabSize().x, 0.0f, 0.0f);
            BlockColumn blockColumn = BlockFactory.CreateColumn(RowAmount, columnPos, BlockGridObject.transform);
            BlockColumns[x] = blockColumn;

            for (int y = 0; y < RowAmount; y++)
            {
                // Create a block with correct position
                Vector3 rowPos = new Vector3(0.0f, y * BlockFactory.GetBlockPrefabSize().y, 0.0f);
                Block block = BlockFactory.CreateBlock(rowPos, blockColumn.gameObject.transform, this);
                block.SetPosition(x, y);
                BlockColumns[x].Blocks[y] = block;
     
            }
        }
    }
}
