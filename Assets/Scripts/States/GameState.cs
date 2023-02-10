using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    protected GameStateManager _stateManager;
    protected GameStateFactory _stateFactory;
    protected BlockGrid _blockGrid;
    public GameState(GameStateManager stateManager, GameStateFactory gameStateFactory, BlockGrid blockGrid)
    {
        _stateManager = stateManager;
        _stateFactory = gameStateFactory;
        _blockGrid = blockGrid;
    }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void CheckTransitions();
    protected void TransitionState(GameState newState)
    {
        // Exit current state, enter new state,
        // then change the CurrentState reference in stateManager

        if(newState == null)
            return;

     //   Debug.Log("Changing State from " + this.ToString() + " to " + newState.ToString());

        _stateManager.PreviousState = this;

        ExitState();

        newState.EnterState();

        _stateManager.CurrentState = newState;
    }

    public abstract void CheckInput();
    public abstract void UpdateState();
}
