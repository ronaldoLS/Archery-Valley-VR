using UnityEngine;
using UnityEngine.InputSystem;

public class RayController : MonoBehaviour
{
    [Header("Ray")]
    [SerializeField] private GameObject rayInteractor;

    [Header("Input")]
    [SerializeField] private InputActionProperty primaryButton;
    [SerializeField] private InputActionProperty thumbstickTouched;

    private bool canControlRay;

    private void OnEnable()
    {
        primaryButton.action.performed += OnInput;
        primaryButton.action.canceled += OnInput;

        thumbstickTouched.action.performed += OnInput;
        thumbstickTouched.action.canceled += OnInput;

        primaryButton.action.Enable();
        thumbstickTouched.action.Enable();
    }

    private void OnDisable()
    {
        primaryButton.action.performed -= OnInput;
        primaryButton.action.canceled -= OnInput;

        thumbstickTouched.action.performed -= OnInput;
        thumbstickTouched.action.canceled -= OnInput;

        primaryButton.action.Disable();
        thumbstickTouched.action.Disable();
    }

    private void OnInput(InputAction.CallbackContext context)
    {
        if (!canControlRay)
            return;

        rayInteractor.SetActive(context.ReadValueAsButton());
    }

    public void SetControl(bool canControl)
    {
        canControlRay = canControl;

        if (!canControl)
            rayInteractor.SetActive(false);
    }
}