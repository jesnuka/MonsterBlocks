using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_StartGame : GameState
{

    public GameState_StartGame(GameStateManager stateManager, GameStateFactory gameStateFactory) : base(stateManager, gameStateFactory) { }

    public override void EnterState()
    {
        _stateManager.GameStarted = false;
    }

    public override void ExitState()
    {

    }
    public override void CheckTransitions()
    {
        if (_stateManager.BlocksInitialized)
        {
            _stateManager.BlocksInitialized = false;
            TransitionState(_stateFactory.StateDropBlocks());
        }

      //  if (_stateManager.GamePaused)
      //      TransitionState(_stateFactory.StatePaused());
    }
    public override void CheckInput()
    {

    }

    public override void UpdateState()
    {

    }
}
