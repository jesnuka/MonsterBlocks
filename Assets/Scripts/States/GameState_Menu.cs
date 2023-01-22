using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_Menu : GameState
{
    public GameState_Menu(GameStateManager stateManager, GameStateFactory gameStateFactory) : base(stateManager, gameStateFactory) { }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }
    public override void CheckTransitions()
    {
       // if(isStartGame)
       //     transitionstate(InitializeBlocks);
    }

    public override void CheckInput()
    {

    }

    public override void UpdateState()
    {

    }
}
