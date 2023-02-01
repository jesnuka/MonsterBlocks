using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Menu : GameState
{
    public GameState_Menu(GameStateManager stateManager, GameStateFactory gameStateFactory, BlockGrid blockGrid) : base(stateManager, gameStateFactory, blockGrid) { }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }
    public override void CheckTransitions()
    {
        if (_stateManager.GameStarted)
        {
            _stateManager.GameStarted = false;
            TransitionState(_stateFactory.StateStartGame());
        }
    }

    public override void CheckInput()
    {

    }

    public override void UpdateState()
    {

    }
}
