using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [Header("References")]
    [field:SerializeField] private BlockGrid _blockGrid;
    public BlockGrid BlockGrid { get { return _blockGrid; } }

    [field: SerializeField] private BlockShapeController _blockController;
    public BlockShapeController BlockController { get { return _blockController; } }

    [field: SerializeField] private MenuManager _menuManager;
    public MenuManager MenuManager { get { return _menuManager; } }

    /* [Header("States")]
     [SerializeField] static GameState_Menu gameState_Menu;
     [SerializeField] static GameState_Paused gameState_Paused;
     [SerializeField] static GameState_StartGame gameState_StartGame;
     [SerializeField] static GameState_DropBlocks gameState_DropBlocks;
     [SerializeField] static GameState_LineCheck gameState_LineCheck;
     [SerializeField] static GameState_LostGame gameState_LostGame;*/

    // Events
    public static event Action<GameState> OnGameStateChanged;
    public static event Action OnBlocksInitialized;

    // Flags
    private bool _gameStarted;
    public bool GameStarted { get { return _gameStarted; } set { _gameStarted = value; } }

    private bool _gamePaused;
    public bool GamePaused { get { return _gamePaused; } set { _gamePaused = value; } }

    private bool _blocksInitialized;
    public bool BlocksInitialized { get { return _blocksInitialized; } set { _blocksInitialized = value; } }

    private bool _returnedToMenu;
    public bool ReturnedToMenu { get { return _returnedToMenu; } set { _returnedToMenu = value; } }

    // Variables
    private GameState _previousState;
    public GameState PreviousState { get { return _previousState; } set { _previousState = value; } }

    private GameState _currentState;
    public GameState CurrentState { get { return _currentState; } set { _currentState = value; } }
    GameStateFactory _states;

    private void Awake()
    {
        Initialize();
    }
    void Start()
    {
        HandleEvents();
    }

    private void HandleEvents()
    {
        MenuManager.OnStartGame += StartGame;
        MenuManager.OnExitGame += ExitGame;

        MenuManager.OnPauseGame += PauseGame;
        MenuManager.OnUnpauseGame += UnpauseGame;

        MenuManager.OnReturnToMenu += ReturnToMenu;

        BlockGrid.OnBlocksInitialized += InitializeBlocks;
    }

    private void Initialize()
    {
        // Setup the state machine
        _states = new GameStateFactory(this, BlockGrid);

        CurrentState = _states.StateMenu();
        CurrentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.CheckTransitions();
        CurrentState.CheckInput();
        CurrentState.UpdateState();
    }

    // Debug Public for now
  //  private void InitializeBlocks()
    public void InitializeBlocks()
    {
        OnBlocksInitialized?.Invoke();
        BlocksInitialized = true;
    }

    private void ReturnToMenu()
    {
        ReturnedToMenu = true;
    }

    private void PauseGame()
    {
        GamePaused = true;
    }

    private void UnpauseGame()
    {
        GamePaused = false;
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        Debug.Log("started game");
        GameStarted = true;
    }
}
