using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuContainer_MainMenu : MenuContainer
{
    public override void CheckInput()
    {

    }

    public override void CheckTransitions()
    {
        if (_menuManager.GameSelectionOpened)
            ChangeMenu(_menuManager.GameSelectionMenu);
     //   if (_menuManager.PreviousMenuOpened) 
     //       _menuManager.PreviousMenuOpened = false;
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
