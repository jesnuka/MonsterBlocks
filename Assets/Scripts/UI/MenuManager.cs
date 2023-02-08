using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class MenuManager : MonoBehaviour
{
    // Menu Containers are basically different Menu States
    [Header("Menu Containers")]
    [field: SerializeField] private MenuContainer _mainMenu;
    public MenuContainer MainMenu { get { return _mainMenu; }}

    [field: SerializeField] private MenuContainer _gameSelectionMenu;
    public MenuContainer GameSelectionMenu { get { return _gameSelectionMenu; } }

    // This is the actual gameplay screen
    [field: SerializeField] private MenuContainer _gameplayMenu;
    public MenuContainer GameplayMenu { get { return _gameplayMenu; } }

    [field: SerializeField] private MenuContainer _pauseMenu;
    public MenuContainer PauseMenu { get { return _pauseMenu; } }

    [field: SerializeField] private MenuContainer _lostGameMenu;
    public MenuContainer LostGameMenu { get { return _lostGameMenu; } }

    // Loading screen displayed during initialization of the game
    [field: SerializeField] private MenuContainer _startingLoadingScreen;
    public MenuContainer StartingLoadingScreen { get { return _startingLoadingScreen; } }


    [Header("Menus")]
    private MenuContainer _currentMenu;
    public MenuContainer CurrentMenu { get { return _currentMenu; } set { _currentMenu = value; } }

    private MenuContainer _previousMenu;
    public MenuContainer PreviousMenu { get { return _previousMenu; } }

    // Events
    // Goes back one level in the menus
    public static event Action OnReturnToPreviousMenu;
    public static event Action OnEnterGameSelection;
    public static event Action OnEnterPreviousMenu;
    public static event Action OnStartGame;
    public static event Action OnExitGame;
    public static event Action OnPauseGame;
    public static event Action OnUnpauseGame;
    public static event Action OnReturnToMenu;

    // Flags

    private bool _gameStarted;
    public bool GameStarted { get { return _gameStarted; } set { _gameStarted = value; } }

    private bool _gameInitialized;
    public bool GameInitialized { get { return _gameInitialized; } set { _gameInitialized = value; } }

    private bool _gamePaused;
    public bool GamePaused { get { return _gamePaused; } set { _gamePaused = value; } }

    private bool _gameLost;
    public bool GameLost { get { return _gameLost; } set { _gameLost = value; } }

    private bool _gameSelectionOpened;
    public bool GameSelectionOpened { get { return _gameSelectionOpened; } set { _gameSelectionOpened = value; } }

    private bool _previousMenuOpened;
    public bool PreviousMenuOpened { get { return _previousMenuOpened; } set { _previousMenuOpened = value; } }

    private bool _returnedToMenu;
    public bool ReturnedToMenu { get { return _returnedToMenu; } set { _returnedToMenu = value; } }

    private void Start()
    {
        HandleEvents();
        InitializeMenus();

        if(_mainMenu != null)
            _currentMenu = _mainMenu;
    }

    private void HandleEvents()
    {
        GameStateManager.OnBlocksInitialized += InitializeGame;
        GameStateManager.OnLostGame += LostGame;
    }

    private void InitializeMenus()
    {
        if(_mainMenu != null)
            _mainMenu._menuManager = this;
        if (_gameSelectionMenu != null)
            _gameSelectionMenu._menuManager = this;
        if (_gameplayMenu != null)
            _gameplayMenu._menuManager = this;
        if (_pauseMenu != null)
            _pauseMenu._menuManager = this;
    }

    public void ReturnToPreviousMenu()
    {
        PreviousMenuOpened = true;
        OnReturnToPreviousMenu?.Invoke();
    }

    public void ReturnToMainMenu()
    {
        ReturnedToMenu = true;
        OnReturnToMenu?.Invoke();
    }

    public void EnterGameSelection()
    {
        GameSelectionOpened = true;
        OnEnterGameSelection?.Invoke();
    }
    public void PauseGame()
    {
        OnPauseGame?.Invoke();
        GamePaused = true;
    }

    public void UnpauseGame()
    {
        OnUnpauseGame?.Invoke();
        GamePaused = false;
    }

    public void LostGame()
    {
        GameLost = true;
    }

    public void ExitGame()
    {
        OnExitGame?.Invoke();
        Application.Quit();
    }

    private void InitializeGame()
    {
        GameInitialized = true;
    }

    public void StartGame()
    {
        OnStartGame?.Invoke();
        Debug.Log("started game");
        GameStarted = true;
    }

    private void Update()
    {
        CurrentMenu?.CheckTransitions(); 
        CurrentMenu?.CheckInput(); 
        CurrentMenu?.UpdateMenu(); 
    }

}
