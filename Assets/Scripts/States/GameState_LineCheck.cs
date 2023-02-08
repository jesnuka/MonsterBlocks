using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_LineCheck : GameState
{
    public GameState_LineCheck(GameStateManager stateManager, GameStateFactory gameStateFactory, BlockGrid blockGrid) : base (stateManager, gameStateFactory, blockGrid) { }

    public override void EnterState()
    {
        _stateManager.LinesCheckStarted = false;
        _stateManager.LinesChecked = false;
        _stateManager.LinesCleared = false;
        _blockGrid.BlockLineChecker.StartLineChecking(_stateManager.BlockShapeController.CurrentBlockShape);
    }

    public override void ExitState()
    {

    }
    public override void CheckTransitions()
    {
        
        if (_stateManager.GamePaused)
            TransitionState(_stateFactory.StatePaused());
        if (_stateManager.LinesCleared)
        {
            _stateManager.LinesCheckStarted = false;
            _stateManager.LinesChecked = false;
            _stateManager.LinesCleared = false; 
            TransitionState(_stateFactory.StateDropBlocks());
         //   TransitionState(_stateFactory.StateMoveShape());
        }
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
