using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    protected GameStateManager _stateManager;
    protected GameStateFactory _stateFactory;
    public GameState(GameStateManager stateManager, GameStateFactory gameStateFactory)
    {
        _stateManager = stateManager;
        _stateFactory = gameStateFactory;
    }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void CheckTransitions();
    protected void TransitionState(GameState newState)
    {
        // Exit current state, enter new state, then change the CurrentState reference in stateManager

        if(newState == null)
            return;

        ExitState();

        newState.EnterState();

        _stateManager.CurrentState = newState;
    }

    public abstract void CheckInput();
    public abstract void UpdateState();
}
