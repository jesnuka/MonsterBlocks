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
            ChangeMenu(_menuManager.MainMenu);
    }

    public override void CloseMenuExtras()
    {
        _menuManager.GameSelectionOpened = false;
    }

    public override void OpenMenuExtras()
    {

    }

    public override void UpdateMenu()
    {

    }
}
