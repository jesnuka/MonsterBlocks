using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_LostGame : GameState
{
    public GameState_LostGame(GameStateManager stateManager, GameStateFactory gameStateFactory, BlockGrid blockGrid) : base(stateManager, gameStateFactory, blockGrid) { }
    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }
    public override void CheckTransitions()
    {
        if (_stateManager.ReturnedToMenu)
        {
            _stateManager.ReturnedToMenu = false;
            TransitionState(_stateFactory.StateMenu());
        }
    }
    public override void CheckInput()
    {

    }
    public override void UpdateState()
    {

    }
}
