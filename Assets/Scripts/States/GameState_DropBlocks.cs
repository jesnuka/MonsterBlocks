using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_DropBlocks : GameState
{

    public GameState_DropBlocks(GameStateManager stateManager, GameStateFactory gameStateFactory) : base(stateManager, gameStateFactory) { }
    public override void EnterState()
    {
        _stateManager.BlocksInitialized = false;
    }

    public override void ExitState()
    {

    }
    public override void CheckTransitions()
    {
        // TODO: ADD PAUSE TO ALL GAMESTATES THAT CAN PAUSE
        if (_stateManager.GamePaused)
            TransitionState(_stateFactory.StatePaused());
    }

    public override void CheckInput()
    {

    }
    public override void UpdateState()
    {

    }
}
