using System.Collections;
using Google.XR.Cardboard;
using UnityEngine;
using UnityEngine.XR.Management;

public class VrModeController : MonoBehaviour
{
    public static VrModeController Instance { get; private set; }

    private bool IsVrModeEnabled => XRGeneralSettings.Instance.Manager.isInitializationComplete;

    private float _defaultFov;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _defaultFov = Camera.main.fieldOfView;
    }

    private void Update()
    {
        if (IsVrModeEnabled)
        {
            if (Api.IsCloseButtonPressed)
            {
                ExitVR();
            }

            if (Api.IsGearButtonPressed)
            {
                Api.ScanDeviceParams();
            }

            Api.UpdateScreenParams();
        }
    }

    public void EnterVR()
    {
#if !UNITY_EDITOR
        if (!Api.HasDeviceParams())
        {
            Api.ScanDeviceParams();
        }

        StartCoroutine(StartXR());

        if (Api.HasNewDeviceParams())
        {
            Api.ReloadDeviceParams();
        }
#endif
    }

    public void ExitVR()
    {
#if !UNITY_EDITOR
        if (IsVrModeEnabled)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }

        Camera.main.ResetAspect();
        Camera.main.fieldOfView = _defaultFov;
        Camera.main.ResetProjectionMatrix();
        Camera.main.ResetWorldToCameraMatrix();
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
#endif
    }

    private IEnumerator StartXR()
    {
        if (!IsVrModeEnabled)
        {
            yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
        }

        XRGeneralSettings.Instance.Manager.StartSubsystems();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.brightness = 1.0f;
    }
}
