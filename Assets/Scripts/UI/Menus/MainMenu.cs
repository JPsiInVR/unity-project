using UnityEngine;

public class MainMenu : Menu
{
    public void OnStartClick()
    {
        MenuController.Instance.DisableAndEnableMenu(Type, MenuType.Loading, true);
    }

    public void OnOptionsClick()
    {
        MenuController.Instance.DisableAndEnableMenu(Type, MenuType.Options, true);
    }

    public void OnCreditsClick()
    {
        MenuController.Instance.DisableAndEnableMenu(Type, MenuType.Credits, true);
    }

    public void OnBackClick()
    {
        MenuController.Instance.EnableMenu(MenuType.Exit);
    }
}
