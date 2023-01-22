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


    [Header("Menus")]
    private MenuContainer _currentMenu;
    public MenuContainer CurrentMenu { get { return _currentMenu; } set { _currentMenu = value; } }

    private MenuContainer _previousMenu;
    public MenuContainer PreviousMenu { get { return _previousMenu; } }

    // Events
    // Goes back one level in the menus
    public static event Action OnReturnToPreviousMenu;
    public static event Action OnEnterGameSelection;
    public static event Action OnStartGame;
    public static event Action OnExitGame;
    public static event Action OnEnterPreviousMenu;

    // Flags

    private bool _gameStarted;
    public bool GameStarted { get { return _gameStarted; } set { _gameStarted = value; } }

    private bool _gamePaused;
    public bool GamePaused { get { return _gamePaused; } set { _gamePaused = value; } }

    private bool _gameUnpaused;
    public bool GameUnpaused { get { return _gameUnpaused; } set { _gameUnpaused = value; } }

    private bool _gameSelectionOpened;
    public bool GameSelectionOpened { get { return _gameSelectionOpened; } set { _gameSelectionOpened = value; } }

    private bool _previousMenuOpened;
    public bool PreviousMenuOpened { get { return _previousMenuOpened; } set { _previousMenuOpened = value; } }

    private void Start()
    {
        InitializeMenus();

        if(_mainMenu != null)
            _currentMenu = _mainMenu;
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

    public void EnterGameSelection()
    {
        GameSelectionOpened = true;
        OnEnterGameSelection?.Invoke();
    }

    public void StartGame()
    {
        GameStarted = true;
        OnStartGame?.Invoke();
    }
    public void ExitGame()
    {
        OnExitGame?.Invoke();
    }

    private void Update()
    {
        CurrentMenu?.CheckTransitions(); 
        CurrentMenu?.CheckInput(); 
        CurrentMenu?.UpdateMenu(); 
    }

}
