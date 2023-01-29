using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuContainer_Gameplay : MenuContainer
{
    public override void CheckInput()
    {

    }

    public override void CheckTransitions()
    {
        Debug.Log("Here");
        if (_menuManager.GamePaused)
            ChangeMenu(_menuManager.PauseMenu);
        if (_menuManager.PreviousMenuOpened)
        {
            _menuManager.PreviousMenuOpened = false;
            ChangeMenu(_menuManager.PauseMenu);
        }
    }

    public override void CloseMenuExtras()
    {

    }

    public override void OpenMenuExtras()
    {

    }

    public override void UpdateMenu()
    {

    }
}