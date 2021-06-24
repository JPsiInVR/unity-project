using System;
using UnityEngine;
using UnityEngine.Events;

public class CardboardInteractable : MonoBehaviour
{
    public UnityEvent onHoverEnter = new UnityEvent();
    public UnityEvent onHoverExit = new UnityEvent();
    public UnityEvent onSelectEnter = new UnityEvent();
    public UnityEvent onSelectExit = new UnityEvent();

    public static event Action OnHoverEnter;
    public static event Action<float> OnHoverStay;
    public static event Action OnHoverExit;

    [SerializeField]
    private float gazeTime;

    private float _gazeStartTime;
    private float _gazeProgress;
    private bool _isHovering;
    private bool _isSelecting;

    private void Update()
    {
        if(_isHovering && _gazeStartTime >= 0)
        {
            if (Time.time - _gazeStartTime > gazeTime || XRCardboardController.Instance.IsTriggerPressed())
            {
                _gazeProgress = 1;
                HandleInput();
            }
            else
            {
                _gazeProgress = (Time.time - _gazeStartTime) / gazeTime;
            }

            OnHoverStay?.Invoke(_gazeProgress);
        }
    }

    public void PointerEnter()
    {
        _isHovering = true;
        _gazeStartTime = Time.time;
        onHoverEnter.Invoke();
        OnHoverEnter?.Invoke();
    }

    public void PointerExit()
    {
        _isHovering = false;
        _gazeStartTime = -1;
        onHoverExit.Invoke();
        OnHoverExit?.Invoke();
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
