using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuContainer : MonoBehaviour
{
    public MenuManager _menuManager;

    void OpenMenu()
    {
        OpenMenuExtras();
        gameObject.SetActive(true);
    }

    public abstract void OpenMenuExtras();

    void CloseMenu()
    {
        CloseMenuExtras();
        _menuManager.PreviousMenuOpened = false;
        gameObject.SetActive(false);
    }
    public abstract void CloseMenuExtras();

    public abstract void CheckTransitions();
    protected void ChangeMenu(MenuContainer newMenu)
    {
        // Exit current menu, enter new menu,
        // then change the CurrentMenu reference in menuManager

        if (newMenu == null)
            return;

        CloseMenu();
        newMenu.OpenMenu();
        _menuManager.CurrentMenu = newMenu;
    }
    public abstract void CheckInput();

    public abstract void UpdateMenu();
}
