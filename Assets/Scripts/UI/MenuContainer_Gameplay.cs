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
        if (_menuManager.GamePaused)
            ChangeMenu(_menuManager.PauseMenu);
        if (_menuManager.PreviousMenuOpened)
        {
            _menuManager.PreviousMenuOpened = false;
            ChangeMenu(_menuManager.PauseMenu);
        }
        if (_menuManager.GameLost)
        {
            _menuManager.GameLost = false;
            ChangeMenu(_menuManager.LostGameMenu);
        }
    }

    public override void CloseMenuExtras()
    {

    }

    public override void OpenMenuExtras()
    {
        _menuManager.GameLost = false;
        _menuManager.PreviousMenuOpened = false;
    }

    public override void UpdateMenu()
    {

    }
}
