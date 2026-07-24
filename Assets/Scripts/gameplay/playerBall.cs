using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerBall : MonoBehaviour
{
    public InputActionAsset inputs;

    private InputAction input_shoot;

    private void OnEnable()
    {
        inputs.FindActionMap("Player").Enable();
    }

    private void Awake()
    {
        input_shoot = InputSystem.actions.FindAction("Shoot");
    }

    private void Update()
    {
        if (input_shoot.IsPressed())
        {
            Debug.Log("a");
        }
    }
}
