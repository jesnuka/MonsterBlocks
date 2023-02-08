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
        {
            _menuManager.GameSelectionOpened = false;
            ChangeMenu(_menuManager.GameSelectionMenu);
        }
     //   if (_menuManager.PreviousMenuOpened) 
     //       _menuManager.PreviousMenuOpened = false;
    }

    public override void CloseMenuExtras()
    {

    }

    public override void OpenMenuExtras()
    {
        _menuManager.GameSelectionOpened = false;
    }

    public override void UpdateMenu()
    {

    }
}
