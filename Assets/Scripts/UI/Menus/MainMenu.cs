using UnityEngine;

public class MainMenu : Menu
{
    [SerializeField]
    private SceneData sceneToLoad;

    public void OnStartClick()
    {
        MenuController.Instance.DisableMenu(Type);
        SceneController.Instance.Load(sceneToLoad, (data) => { VrModeController.Instance.EnterVR(); }, MenuType.Loading);
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
