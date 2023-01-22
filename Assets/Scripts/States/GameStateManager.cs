using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [Header("States")]
    [SerializeField] static GameState_Menu gameState_Menu;
    [SerializeField] static GameState_Paused gameState_Paused;
    [SerializeField] static GameState_StartGame gameState_StartGame;
    [SerializeField] static GameState_DropBlocks gameState_DropBlocks;
    [SerializeField] static GameState_LineCheck gameState_LineCheck;
    [SerializeField] static GameState_LostGame gameState_LostGame;



    // Events
    public static event Action<GameState> OnGameStateChanged;
    public static event Action OnStartGame;
    public static event Action OnExitGame;
    public static event Action OnPauseGame;
    public static event Action OnUnpauseGame;


    // Flags
    private bool _isGameStarted;
    public bool IsGameStarted { get { return _isGameStarted; } }

    private bool _isGamePaused;
    public bool IsGamePaused { get { return _isGamePaused; } }

    private bool _isBlocksInitialized;
    public bool IsBlocksInitialized { get { return _isBlocksInitialized; }}

    // Variables
    private GameState _currentState;
    public GameState CurrentState { get { return _currentState; } set { _currentState = value; } }
    GameStateFactory _states;

    private void Awake()
    {
        // Setup the state machine
        _states = new GameStateFactory(this);
        _currentState = _states.StateMenu();
        _currentState.EnterState();
    }
    void Start()
    {
        Initialize();
    }

    private void HandleEvents()
    {
        OnStartGame += StartGame;
        OnExitGame += ExitGame;

    }

    private void Initialize()
    {
        HandleEvents();
     //   _currentState = gameState_Paused;
    }

    private void ChangeGameState(GameState newState)
    {
        //   previousState = currentState;
        CurrentState.ExitState();
        _currentState = newState;
        CurrentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.CheckTransitions();
        CurrentState.CheckInput();
        CurrentState.UpdateState();
    }

    private void PauseGame()
    {
        _isGamePaused = true;
    }

    private void UnPause()
    {
        _isGamePaused = false;
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        Debug.Log("started game");
        _isGameStarted = true;
    }
    private void BlocksInitialized()
    {
        _isGameStarted = false;
        _isBlocksInitialized = false;
    }
}
