using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Paused : GameState
{
    public GameState_Paused(GameStateManager stateManager, GameStateFactory gameStateFactory) : base(stateManager, gameStateFactory) { }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {
      //  StateManager.OnGameStateChanged?.Invoke(newState);

    }
    public override void CheckTransitions()
    {

    }

    public override void CheckInput()
    {

    }
    public override void UpdateState()
    {

    }
}
