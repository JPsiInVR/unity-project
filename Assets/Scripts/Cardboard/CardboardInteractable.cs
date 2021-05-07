using UnityEngine;
using UnityEngine.Events;

public class CardboardInteractable : MonoBehaviour
{
    public float gazeTime;
    public UnityEvent onHoverEnter = new UnityEvent();
    public UnityEvent onHoverExit = new UnityEvent();
    public UnityEvent onSelectEnter = new UnityEvent();
    public UnityEvent onSelectExit = new UnityEvent();

    private float _gazeStartTime;
    private bool _isHovering;
    private bool _isSelecting;

    public void PointerEnter()
    {
        _isHovering = true;
        _gazeStartTime = Time.time;
        RadialProgress.Instance.PlayDefault();
        onHoverEnter.Invoke();
    }

    public void PointerExit()
    {
        _isHovering = false;
        _gazeStartTime = -1;
        RadialProgress.Instance.PlayFadeOut();
        onHoverExit.Invoke();
    }

    private void Update()
    {
        if (XRCardboardController.Instance.IsTriggerPressed())
        {
            HandleInput();
        }

        if(_isHovering && _gazeStartTime >= 0)
        {
            if (Time.time - _gazeStartTime > gazeTime)
            {
                RadialProgress.Instance.Progress = 1;
                RadialProgress.Instance.PlayFadeOut();
                HandleInput();
            }
            else
            {
                RadialProgress.Instance.Progress = (Time.time - _gazeStartTime) / gazeTime;
            }
        }
    }

    private void HandleInput()
    {
        if (_isSelecting)
        {
            _gazeStartTime = -1;
            _isSelecting = false;
            onSelectExit.Invoke();
        }
        else if (_isHovering)
        {
            _gazeStartTime = -1;
            _isSelecting = true;
            onSelectEnter.Invoke();
        }
    }
}
