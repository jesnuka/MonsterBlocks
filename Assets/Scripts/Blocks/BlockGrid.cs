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
    public BlockShapeController BlockShapeController { get { return _blockShapeController; } }

    [SerializeField] private BlockLineChecker _blockLineChecker;
    public BlockLineChecker BlockLineChecker { get { return _blockLineChecker; } set { _blockLineChecker = value; } }

    [field:SerializeField] private BlockDropper _blockDropper;
    public BlockDropper BlockDropper { get { return _blockDropper; }}

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
        BlockShapeController.SetupController(this, BlockFactory);

        if (BlockLineChecker == null)
            BlockLineChecker = new BlockLineChecker(this);
        if (BlockDropper != null)
            BlockDropper.Initialize(this);

        // Align game board according to the size
        BlockGridObject.offsetMax = new Vector2(
        BlockGridObject.offsetMax.x,
        -((RowAmount - 3) * BlockFactory.GetBlockPrefabSize().y));

        // Aligh background 
        BlockGridBackground.sizeDelta = 
            new Vector2
            (
                (ColumnAmount) * BlockFactory.GetBlockPrefabSize().x,
                (RowAmount - 2) * BlockFactory.GetBlockPrefabSize().y
            );

        CreateSpawnPosition(ColumnAmount % 2);

    }

    private void ChangeGameSettings(GameSettings gameSettings)
    {
        GameSettings = gameSettings;
    }

    public void ResetGrid()
    {
        BlockShapeController.ResetController();

        foreach (BlockColumn blockColumn in BlockColumns)
            Destroy(blockColumn.gameObject);
    }

    public List<Block> ReturnEnabledBlocks()
    {
        List<Block> enabledBlocks = new List<Block>();
        for(int x = 0; x < ColumnAmount; x++)
        {
            for (int y = 0; y < RowAmount; y++)
            {
                if (BlockColumns[x].Blocks[y].IsEnabled)
                    enabledBlocks.Add(BlockColumns[x].Blocks[y]);
            }
        }

        return enabledBlocks;
    }

    public bool CheckCollision(BlockPosition[] newPositions)
    {
        return CollisionCheck(newPositions);
    }

    public bool CheckCollision(BlockPosition newPosition)
    {
        if (newPosition == null)
            return false;
        BlockPosition[] newPositions = new BlockPosition[1];
        newPositions[0] = newPosition;
        return CollisionCheck(newPositions);
    }
    private bool CollisionCheck(BlockPosition[] newPositions)
    {
        // Check if block already exist in the BlockShape position, or the position is out of bounds
        foreach (BlockPosition blockPosition in newPositions)
            if (GetBlock(blockPosition.Column, blockPosition.Row).IsEnabled ||
                blockPosition.Column >= ColumnAmount ||
                blockPosition.Row >= RowAmount ||
                blockPosition.Row < 0 ||
                blockPosition.Column < 0)
                return false;

        return true;
    }

    public BlockPosition GetMovedBlockPosition(Block block, int xDirection, int yDirection)
    {
        BlockPosition movedBlockPosition = new BlockPosition(block.BlockPosition.Column, block.BlockPosition.Row);
        int column = block.BlockPosition.Column + xDirection;
        int row = block.BlockPosition.Row + yDirection;
        
        if (column >= ColumnAmount || column < 0 || row >= RowAmount || row < 0)
            return null;

        movedBlockPosition.SetPosition(column, row);

        return movedBlockPosition;
    }


    private void CreateSpawnPosition(int odd)
    {
        int column = (ColumnAmount / 2) - 2;

        if (column < 0)
            column = 0;
        // Spawn position is at the top of the grid, but offset by the size of BlockShapes
        SpawnPosition = new BlockPosition(column, RowAmount-4);
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

        // Create spawn zone at the top of the grid
        for (int x = 0; x < ColumnAmount; x++)
        {
            for (int y = RowAmount - 2; y < RowAmount; y++)
                BlockColumns[x].Blocks[y].DisableBlock();
        }
    }

    public Block GetBlock(int column, int row)
    {
        if (column < ColumnAmount && column >= 0 && row < RowAmount && row >= 0)
            return BlockColumns[column].Blocks[row];
        else
            return null;
    }
}
