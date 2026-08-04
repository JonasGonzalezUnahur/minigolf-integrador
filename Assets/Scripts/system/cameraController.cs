using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class cameraController : MonoBehaviour
{
    [Header("Components")]
    public Camera cameraObj;

    [Header("image")]
    public GameObject target;

    [Header("Proporties")]
    public float moveSpeed = 10f;
    public bool freeroam = false;

    [Header("Inputs")]
    public InputActionAsset inputs;
    private InputAction toggleMode;
    private InputAction cameraLeft;
    private InputAction cameraRight;
    private InputAction cameraDown;
    private InputAction cameraUp;

    public void Awake()
    {
        toggleMode = InputSystem.actions.FindAction("ToggleCamera");
        cameraLeft = InputSystem.actions.FindAction("CameraLeft");
        cameraRight = InputSystem.actions.FindAction("CameraRight");
        cameraDown = InputSystem.actions.FindAction("CameraDown");
        cameraUp = InputSystem.actions.FindAction("CameraUp");

        inputs.FindActionMap("Camera").Disable();
    }

    public void Update()
    {
        if (toggleMode.WasPressedThisFrame())
        {
            ToggleFreeroam();
        }
        if (!freeroam)
        {
            cameraObj.transform.position = target.transform.position;
        } else
        {
            if (cameraLeft.IsPressed())
            {
                cameraObj.transform.position -= new Vector3(moveSpeed, 0) * Time.deltaTime;
            }
            if (cameraRight.IsPressed())
            {
                cameraObj.transform.position += new Vector3(moveSpeed, 0) * Time.deltaTime;
            }
            if (cameraDown.IsPressed())
            {
                cameraObj.transform.position -= new Vector3(0, moveSpeed) * Time.deltaTime;
            }
            if (cameraUp.IsPressed())
            {
                cameraObj.transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;
            }
        }
        
    }

    public void Changetarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public void ToggleFreeroam()
    {
        freeroam = !freeroam;
        if (freeroam)
        {
            inputs.FindActionMap("Camera").Enable();
            inputs.FindActionMap("Player").Disable();
            toggleMode = InputSystem.actions.FindAction("ToggleCamera");
        }
        else
        {
            inputs.FindActionMap("Camera").Disable();
            inputs.FindActionMap("Player").Enable();
            toggleMode = InputSystem.actions.FindAction("ToggleCamera");
        }
    }
}
