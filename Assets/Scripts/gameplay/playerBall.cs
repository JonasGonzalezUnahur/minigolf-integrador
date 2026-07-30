using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class playerBall : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb2d;
    public GameObject reticle;
    public SpriteRenderer ballSprite;
    public StateMachine stateMachine { get; private set; }

    [Header("Inputs")]
    public InputActionAsset inputs;
    public InputAction inputShoot;
    private InputAction inputAimLeft;
    private InputAction inputAimRight;

    [Header("Properties")]
    public float charge = 1;
    public float basePower = 50;
    public int playerNmr = 0;

    [Header("Gameplay")]
    public Vector2 lastPosition;
    public int shots = 0;
    public int totalShots = 0;

    [Header("Events")]
    public UnityEvent EventBallStopped;

    private void Awake()
    {
        stateMachine = new StateMachine();
        //stateMachine.ChangeState(new AimingState(this));

        inputShoot = InputSystem.actions.FindAction("Shoot");
        inputAimLeft = InputSystem.actions.FindAction("AimLeft");
        inputAimRight = InputSystem.actions.FindAction("AimRight");
    }

    private void Start()
    {
        return;
    }

    private void Update()
    {
        stateMachine.Tick();
    }

    public void ShootBall()
    {
        Vector3 angle = reticle.GetComponentInChildren<SpriteRenderer>().transform.position;
        angle = angle - transform.position;
        angle = angle * basePower;
        rb2d.AddForce(angle * charge);
    }

    public void AimBall()
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

    public void ChargeShot()
    {
        bool reverse = false;
        if (!reverse)
        {
            charge += 0.01f;
            if (charge < 1)
            {
                reverse = true;
            }
        } else
        {
            charge -= 0.01f;
            if (charge > 1)
            {
                reverse = false;
            }
        }
    }
}
