#if !UNITY_EDITOR
using Google.XR.Cardboard;
#endif
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class XRCardboardController : MonoBehaviour
{
    public static XRCardboardController Instance { get; private set; }

    public UnityEvent OnTriggerPressed = new UnityEvent();

    [SerializeField]
    private bool _makeAllButtonsClickable = true;
    [SerializeField]
    private bool _raycastEveryUpdate = true;
    [SerializeField]
    private LayerMask _interactablesLayers = -1;
    [SerializeField]
    private float _gazeTime;
    [SerializeField]
    private GraphicRaycaster _graphicRaycaster;
    [SerializeField]
    private EventSystem _eventSystem;

    private bool _foundInteractable = false;
    private GameObject _gazedAtObject = null;
    private bool _isGraphicRaycasterNotNull;
    private const float MAX_RAYCAST_DISTANCE = 10;

    private void Start()
    {
        _isGraphicRaycasterNotNull = _graphicRaycaster != null;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Only one instance of singleton allowed");
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Update()
    {
        if (_raycastEveryUpdate)
        {
            CastForInteractables();
        }

        if (IsTriggerPressed())
        {
            OnTriggerPressed.Invoke();
        }

#if !UNITY_EDITOR
        if (Api.IsCloseButtonPressed)
        {
            //ApplicationQuit();
        }

        if (Api.IsGearButtonPressed)
        {
            Api.ScanDeviceParams();
        }

        if (Api.HasNewDeviceParams())
        {
            Api.ReloadDeviceParams();
        }
#endif
    }

    public bool IsTriggerPressed()
    {
#if UNITY_EDITOR
        return Input.GetMouseButtonDown(0);
#else
        return (Api.IsTriggerPressed || Input.GetMouseButtonDown(0));
#endif
    }

    private void CastForInteractables()
    {
        if (!CastForUIObjects())
        {
            if (!CastForColliderObjects())
            {
                _gazedAtObject?.SendMessage("PointerExit");
                _gazedAtObject = null;
            }
        }
    }

    private bool CastForUIObjects()
    {
        if (_isGraphicRaycasterNotNull)
        {
            PointerEventData pointerEventData = new PointerEventData(_eventSystem);
            List<RaycastResult> results = new List<RaycastResult>();

            pointerEventData.position = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            _graphicRaycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult raycastResult in results)
            {
                if (raycastResult.gameObject.layer == LayerMask.NameToLayer("Interactable"))
                {
                    HandleInteraction(raycastResult.gameObject);
                    return true;
                }
            }
        }
        return false;
    }

    private bool CastForColliderObjects()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, MAX_RAYCAST_DISTANCE, _interactablesLayers))
        {
            HandleInteraction(hit.transform.gameObject);
            return true;
        }

        return false;
    }

    private void HandleInteraction(GameObject interactableGameobject)
    {
        if (_gazedAtObject != interactableGameobject)
        {
            _gazedAtObject?.SendMessage("PointerExit");
            _gazedAtObject = interactableGameobject;
            _gazedAtObject.SendMessage("PointerEnter");
        }
    }
}
