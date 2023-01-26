using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuContainer_StartingLoadingScreen : MenuContainer
{
    [SerializeField] private Animation loadingIcon;
    public override void CheckInput()
    {

    }

    public override void CheckTransitions()
    {
        if (_menuManager.GameInitialized)
        {
            _menuManager.GameInitialized = false;
            ChangeMenu(_menuManager.GameplayMenu);
        }
    }

    public override void CloseMenuExtras()
    {
        loadingIcon.Stop();
    }

    public override void OpenMenuExtras()
    {
        loadingIcon.Play();
    }

    public override void UpdateMenu()
    {

    }
}
