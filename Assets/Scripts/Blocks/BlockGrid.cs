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
    public GameSettings GameSettings { get { return _gameSettings; } set { _gameSettings = value; } }

    [SerializeField] private BlockFactory _blockFactory;
    public BlockFactory BlockFactory { get { return _blockFactory; } }

    [SerializeField] private BlockShapeController _blockShapeController;
    public BlockShapeController BlockShapeController { get { return _blockShapeController; } set { _blockShapeController = value; } }

    [Header("UI Elements")]
    [Tooltip("Contains the block columns, which contain blocks")]
    [SerializeField] private RectTransform _blockGridObject;
    public RectTransform BlockGridObject { get { return _blockGridObject; } }

    [SerializeField] private RectTransform _blockGridBackground;
    public RectTransform BlockGridBackground { get { return _blockGridBackground; } }

    [Header("Game Related")]

    [field:SerializeField] private BlockColumn[] _blockColumns;
    public BlockColumn[] BlockColumns { get { return _blockColumns; } set { _blockColumns = value; } }
    public int ColumnAmount { get { return GameSettings.CurrentColumnAmount; }}
    public int RowAmount { get { return GameSettings.CurrentRowAmount; }}

    // The BlockPosition within the BlockGrid where to spawn BlockShapes
    private BlockPosition _spawnPosition;
    public BlockPosition SpawnPosition { get { return _spawnPosition; } set { _spawnPosition = value; } }

    private void Start()
    {
        GameSettings.OnGameSettingsChanged += ChangeGameSettings;
        MenuManager.OnReturnToMenu += ResetGrid;
    }

    public void SetupGrid()
    {
        if(BlockShapeController == null)
            BlockShapeController = new BlockShapeController(this, BlockFactory);

        // Align game board according to the size
        BlockGridObject.offsetMax = new Vector2(
        BlockGridObject.offsetMax.x,
        -((RowAmount - 1) * BlockFactory.GetBlockPrefabSize().y));

        // Aligh background 
        BlockGridBackground.sizeDelta = 
            new Vector2
            (
                (ColumnAmount) * BlockFactory.GetBlockPrefabSize().x,
                (RowAmount) * BlockFactory.GetBlockPrefabSize().y
            );

        CreateSpawnPosition(ColumnAmount % 2);

    }

    private void ChangeGameSettings(GameSettings gameSettings)
    {
        GameSettings = gameSettings;
    }

    private void ResetGrid()
    {
        foreach (BlockColumn blockColumn in BlockColumns)
            Destroy(blockColumn.gameObject);
    }

    private void CreateSpawnPosition(int odd)
    {
        if (ColumnAmount <= 0)
            return;

        int column = (ColumnAmount / 2) - 2;

        if (column < 0)
            column = 0;

        SpawnPosition = new BlockPosition(column, 0);
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

    public Block GetBlock(int column, int row)
    {
        return BlockColumns[column].Blocks[row];
    }
}
