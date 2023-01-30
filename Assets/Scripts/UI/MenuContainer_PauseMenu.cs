using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuContainer_PauseMenu : MenuContainer
{
    public override void CheckInput()
    {

    }

    public override void CheckTransitions()
    {
        if (!_menuManager.GamePaused)
            ChangeMenu(_menuManager.GameplayMenu);
        if (_menuManager.ReturnedToMenu)
        {
            _menuManager.GamePaused = false;
            _menuManager.ReturnedToMenu = false;
            ChangeMenu(_menuManager.MainMenu);
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
