using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuContainer_GameSelection : MenuContainer
{
    public override void CheckInput()
    {

    }

    public override void CheckTransitions()
    {
        if (_menuManager.GameStarted)
        {
            _menuManager.GameStarted = false;
            ChangeMenu(_menuManager.StartingLoadingScreen);
        }

        if (_menuManager.PreviousMenuOpened)
        {
            _menuManager.PreviousMenuOpened = false;
            ChangeMenu(_menuManager.MainMenu);
        }
    }

    public override void CloseMenuExtras()
    {

    }

    public override void OpenMenuExtras()
    {
        _menuManager.GameStarted = false;
        _menuManager.PreviousMenuOpened = false;
    }

    public override void UpdateMenu()
    {

    }
}
