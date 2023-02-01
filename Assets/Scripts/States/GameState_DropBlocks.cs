using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_DropBlocks : GameState
{

    private bool _blockDropped;
    public bool BlockDropped { get { return _blockDropped; } set { _blockDropped = value; } }

    public GameState_DropBlocks(GameStateManager stateManager, GameStateFactory gameStateFactory, BlockGrid blockGrid) : base(stateManager, gameStateFactory, blockGrid) { }
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

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            _stateManager.MenuManager.PauseGame();

        if (Input.GetKeyDown(KeyCode.DownArrow))
            _blockGrid.BlockShapeController.MoveShapeDown();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            _blockGrid.BlockShapeController.MoveShapeLeft();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            _blockGrid.BlockShapeController.MoveShapeRight();
    }
    public override void UpdateState()
    {
        if(!BlockDropped)
        {
            BlockDropped = true;

            // Spawn block
          //  BlockGrid.spawn
        }

        // Move block down
    }
}
