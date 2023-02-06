using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_DropBlocks : GameState
{
    public GameState_DropBlocks(GameStateManager stateManager, GameStateFactory gameStateFactory, BlockGrid blockGrid) : base (stateManager, gameStateFactory, blockGrid) { }

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

        if (_stateManager.BlocksDropped)
        {
            _stateManager.BlocksBeingDropped = false;
            _stateManager.BlocksDropped = false;
            TransitionState(_stateFactory.StateMoveShape());
        }
    }

    public override void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            _stateManager.MenuManager.PauseGame();
    }

    public override void UpdateState()
    {
        if (!_stateManager.BlocksBeingDropped)
        {
            _stateManager.BlocksBeingDropped = true;
            _blockGrid.BlockDropper.CheckMoveBlocks();
        }
    }
}
