using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerBall : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb2d;
    public GameObject reticle;
    public SpriteRenderer ballSprite;

    [Header("Inputs")]
    public InputActionAsset inputs;
    private InputAction inputShoot;
    private InputAction inputAimLeft;
    private InputAction inputAimRight;

    [Header("Properties")]
    public float power;

    private void OnEnable()
    {
        inputs.FindActionMap("Player").Enable();
    }

    private void Awake()
    {
        inputShoot = InputSystem.actions.FindAction("Shoot");
        inputAimLeft = InputSystem.actions.FindAction("AimLeft");
        inputAimRight = InputSystem.actions.FindAction("AimRight");
    }

    private void Update()
    {
        aimBall();
        if (inputShoot.WasPressedThisFrame())
        {
            shootBall();
        }
    }

    private void shootBall()
    {
        Vector3 target = reticle.GetComponentInChildren<SpriteRenderer>().transform.position;
        target = target - transform.position;
        Debug.Log(target);
        rb2d.AddForce(target * power);
    }

    private void aimBall()
    {
        if (inputAimLeft.IsPressed() && !inputAimRight.IsPressed())
        {
            Vector3 newRotation = new Vector3(0, 0, 50);
            reticle.transform.Rotate(newRotation * Time.deltaTime);
        } else if (!inputAimLeft.IsPressed() && inputAimRight.IsPressed())
        {
            Vector3 newRotation = new Vector3(0, 0, -50);
            reticle.transform.Rotate(newRotation * Time.deltaTime);
        }
        
    }

 
}
