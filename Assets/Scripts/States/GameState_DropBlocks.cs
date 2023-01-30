using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_DropBlocks : GameState
{

    private bool _blockDropped;
    public bool BlockDropped { get { return _blockDropped; } set { _blockDropped = value; } }

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
        if (_stateManager.GamePaused)
            TransitionState(_stateFactory.StatePaused());
    }

    public override void CheckInput()
    {
        // Check for player input to rotate the block
    }
    public override void UpdateState()
    {
        if(!BlockDropped)
        {
            BlockDropped = true;

            // Spawn block
        }

        // Move block down
    }
}
