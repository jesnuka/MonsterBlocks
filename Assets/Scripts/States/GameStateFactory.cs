public class GameStateFactory
{
    GameStateManager _stateManager;

    public GameStateFactory(GameStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    public GameState StateMenu()
    {
        return new GameState_Menu(_stateManager, this);
    }
    public GameState StateLineCheck()
    {
        return new GameState_LineCheck(_stateManager, this);
    }
    public GameState StatePaused()
    {
        return new GameState_Paused(_stateManager, this);
    }
    public GameState StateLostGame()
    {
        return new GameState_LostGame(_stateManager, this);
    }
    public GameState StateDropBlocks()
    {
        return new GameState_DropBlocks(_stateManager, this);
    }
    public GameState StateStartGame()
    {
        return new GameState_StartGame(_stateManager, this);
    }
}
