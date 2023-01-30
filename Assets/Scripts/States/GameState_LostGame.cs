using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_LostGame : GameState
{
    public GameState_LostGame(GameStateManager stateManager, GameStateFactory gameStateFactory) : base(stateManager, gameStateFactory) { }
    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }
    public override void CheckTransitions()
    {
        if(_stateManager.GameStarted)
            TransitionState(_stateFactory.StateMenu());

        if (_stateManager.ReturnedToMenu)
        {
            // TODO: Reset game here, so it is started again next time properly
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
