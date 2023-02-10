using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Paused : GameState
{
    public GameState_Paused(GameStateManager stateManager, GameStateFactory gameStateFactory, BlockGrid blockGrid) : base(stateManager, gameStateFactory, blockGrid) { }

    public override void EnterState()
    {
        _stateManager.ReturnedToMenu = false;
    }

    public override void ExitState()
    {
    }

    public override void CheckTransitions()
    {
        if (!_stateManager.GamePaused)
            TransitionState(_stateManager.PreviousState);

        if (_stateManager.ReturnedToMenu)
        {
            _stateManager.GamePaused = false;
            _stateManager.ReturnedToMenu = false;
            TransitionState(_stateFactory.StateMenu());
        }
    }

    public override void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            _stateManager.MenuManager.UnpauseGame();
    }
    public override void UpdateState()
    {

    }
}
