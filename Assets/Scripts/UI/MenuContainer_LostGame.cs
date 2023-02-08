using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuContainer_LostGame : MenuContainer
{
    public override void CheckInput()
    {

    }

    public override void CheckTransitions()
    {
        if (_menuManager.ReturnedToMenu)
        {
            _menuManager.GameLost = false;
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
        _menuManager.GamePaused = false;
        _menuManager.ReturnedToMenu = false;
    }

    public override void UpdateMenu()
    {

    }
}
