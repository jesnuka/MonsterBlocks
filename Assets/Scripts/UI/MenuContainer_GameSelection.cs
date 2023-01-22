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
            ChangeMenu(_menuManager.GameplayMenu);
        if (_menuManager.PreviousMenuOpened)
            ChangeMenu(_menuManager.MainMenu);
    }

    public override void CloseMenuExtras()
    {
        _menuManager.GameStarted = false;
    }

    public override void OpenMenuExtras()
    {

    }

    public override void UpdateMenu()
    {

    }
}
