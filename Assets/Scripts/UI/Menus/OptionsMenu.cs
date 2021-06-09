public class OptionsMenu : Menu
{
    public void OnBackClick()
    {
        MenuController.Instance.DisableAndEnableMenu(Type, MenuType.Main, true);
    }
}
