public class GameStateFactory
{
    GameStateManager _stateManager;
    BlockGrid _blockGrid;

    public GameStateFactory(GameStateManager stateManager, BlockGrid blockGrid)
    {
        _stateManager = stateManager;
        _blockGrid = blockGrid;
    }

    public GameState StateMenu()
    {
        return new GameState_Menu(_stateManager, this, _blockGrid);
    }
    public GameState StateLineCheck()
    {
        return new GameState_LineCheck(_stateManager, this, _blockGrid);
    }
    public GameState StatePaused()
    {
        return new GameState_Paused(_stateManager, this, _blockGrid);
    }
    public GameState StateLostGame()
    {
        return new GameState_LostGame(_stateManager, this, _blockGrid);
    }
    public GameState StateMoveShape()
    {
        return new GameState_MoveShape(_stateManager, this, _blockGrid);
    }
    public GameState StateDropBlocks()
    {
        return new GameState_DropBlocks(_stateManager, this, _blockGrid);
    }
    public GameState StateStartGame()
    {
        return new GameState_StartGame(_stateManager, this, _blockGrid);
    }
}
