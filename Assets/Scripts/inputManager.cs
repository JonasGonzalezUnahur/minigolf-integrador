using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputManager : MonoBehaviour
{
    public InputActionAsset inputMap;

    private InputAction shootball;
    private InputAction aimBall;

    private void Awake()
    {
        shootball = InputSystem.actions.FindAction("Shoot");
        aimBall = InputSystem.actions.FindAction("Aim");
    }
}
