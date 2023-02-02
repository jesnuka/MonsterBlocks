using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_DropBlocks : GameState
{
    public GameState_DropBlocks(GameStateManager stateManager, GameStateFactory gameStateFactory, BlockGrid blockGrid) : base(stateManager, gameStateFactory, blockGrid) { }
    public override void EnterState()
    {
        // Spawn shape to be moved
        if(!_stateManager.ShapeCreated)
        {
            _blockGrid.BlockShapeController.CreateNewShape();
        }
    }

    public override void ExitState()
    {

    }
    public override void CheckTransitions()
    {
        if (_stateManager.GamePaused)
            TransitionState(_stateFactory.StatePaused());

        if (_stateManager.GameLost)
        {
            _stateManager.GameLost = false;
            TransitionState(_stateFactory.StateLostGame());
        }

        if (_stateManager.ShapePlaced)
        {
            _stateManager.ShapeCreated = false;
            _stateManager.ShapePlaced = false;
            TransitionState(_stateFactory.StateLineCheck());
        }
    }

    public override void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            _stateManager.MenuManager.PauseGame();

        if (Input.GetKeyDown(KeyCode.DownArrow))
            _blockGrid.BlockShapeController.MoveShapeDown();

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            _blockGrid.BlockShapeController.MoveShapeLeft();

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            _blockGrid.BlockShapeController.MoveShapeRight();

        if (Input.GetKeyDown(KeyCode.E))
            _blockGrid.BlockShapeController.RotateShapeClockwise();

        if (Input.GetKeyDown(KeyCode.Q))
            _blockGrid.BlockShapeController.RotateShapeCounterclockwise();
    }
    public override void UpdateState()
    {
     /*   if(!_stateManager.ShapeCreated)
        {
            _blockGrid.BlockShapeController.CreateNewShape();
        }*/

        // Move shape down every so often
        if(!_blockGrid.BlockShapeController.BlockMoveStarted)
            _blockGrid.BlockShapeController.StartBlockTimedMovement();
    }

}
