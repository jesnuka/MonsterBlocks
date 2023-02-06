using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [Header("References")]
    [field:SerializeField] private BlockGrid _blockGrid;
    public BlockGrid BlockGrid { get { return _blockGrid; } }

    [field: SerializeField] private BlockShapeController _blockShapeController;
    public BlockShapeController BlockShapeController { get { return _blockShapeController; } }

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

    private bool _gameLost;
    public bool GameLost { get { return _gameLost; } set { _gameLost = value; } }

    private bool _blocksInitialized;
    public bool BlocksInitialized { get { return _blocksInitialized; } set { _blocksInitialized = value; } }

    private bool _shapePlaced;
    public bool ShapePlaced { get { return _shapePlaced; } set { _shapePlaced = value; } }

    private bool _shapeCreated;
    public bool ShapeCreated { get { return _shapeCreated; } set { _shapeCreated = value; } }

    private bool _linesChecked;
    public bool LinesChecked { get { return _linesChecked; } set { _linesChecked = value; } }

    private bool _linesCheckStarted;
    public bool LinesCheckStarted { get { return _linesCheckStarted; } set { _linesCheckStarted = value; } }

    private bool _linesCleared;
    public bool LinesCleared { get { return _linesCleared; } set { _linesCleared = value; } }

    private bool _blocksDropped;
    public bool BlocksDropped { get { return _blocksDropped; } set { _blocksDropped = value; } }

    private bool _blocksBeingDropped;
    public bool BlocksBeingDropped { get { return _blocksBeingDropped; } set { _blocksBeingDropped = value; } }

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

        BlockShapeController.OnBlockShapePlaced += PlaceShape;
        BlockShapeController.OnBlockShapeCreated += CreateShape;

        BlockLineChecker.OnLinesChecked += LineCheck;
        BlockLineChecker.OnLineCheckStarted += LineCheckStart;
        BlockLineChecker.onLinesCleared += AllLinesCleared;

        BlockDropper.OnNothingMoved += StopBlockDropping;
        BlockDropper.OnBlocksMoved += StoppedBlocksBeingDropped;
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

    private void LostGame()
    {
        GameLost = true;
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

    private void CreateShape()
    {
        ShapeCreated = true;
    }

    private void PlaceShape()
    {
        ShapePlaced = true;
    }
    private void LineCheckStart()
    {
        LinesCheckStarted = true;
    }
    private void LineCheck(int blockCount)
    {
        LinesChecked = true;
    }
    private void AllLinesCleared(int lineCount)
    {
        LinesCleared = true;
    }

    private void StopBlockDropping()
    {
        BlocksDropped = true;
    }

    private void StoppedBlocksBeingDropped()
    {
        BlocksBeingDropped = false;
    }

    private void StartGame()
    {
        Debug.Log("started game");
        GameStarted = true;
    }
}
