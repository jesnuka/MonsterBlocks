using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_LineCheck : GameState
{
    public GameState_LineCheck(GameStateManager stateManager, GameStateFactory gameStateFactory, BlockGrid blockGrid) : base (stateManager, gameStateFactory, blockGrid) { }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }
    public override void CheckTransitions()
    {
        if (_stateManager.GamePaused)
            TransitionState(_stateFactory.StatePaused());
    }

    public override void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            _stateManager.MenuManager.PauseGame();
    }

    public override void UpdateState()
    {

    }
}
