using UnityEngine;

public class AnimationEventHandlers : MonoBehaviour
{
    //This method is fired when credits finish rolling.
    public void HideCredits()
    {
        MenuController.Instance.DisableAndEnableMenu(MenuType.Credits, MenuType.Main, true);
    }

    //This method is fired when user sees the information that he should wear vr glasses for some time.
    public void StartGame()
    {
        Debug.Log("Enable VR Mode, and calibration");
    }
}
