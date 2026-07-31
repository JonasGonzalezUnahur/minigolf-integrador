using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnoffState: IState    
{
    private playerBall playerBall;

    public TurnoffState(playerBall playerBall) { this.playerBall = playerBall; }
    
    public void Enter()
    {
        playerBall.inputs.FindActionMap("Player").Disable();
        playerBall.reticle.SetActive(false);
        playerBall.ballSprite.color = new Color(playerBall.ballSprite.color.r - 0.3f, playerBall.ballSprite.color.g - 0.3f, playerBall.ballSprite.color.b - 0.3f);
    }

    public void Tick()
    {
        return;
        //turnSystem va sacar y poner la bola en este state
    }

    public void Exit()
    {
        playerBall.inputs.FindActionMap("Player").Enable();
        playerBall.ballSprite.color = new Color(1, 1, 1);
    }
}

public class AimingState: IState
{
    private playerBall playerBall;

    public AimingState(playerBall playerBall) { this.playerBall = playerBall; }

    public void Enter()
    {
        playerBall.reticle.SetActive(true);
    }

    public void Tick()
    {
        playerBall.AimBall();
        if (playerBall.inputShoot.WasPressedThisFrame())
        {
            playerBall.stateMachine.ChangeState(new ChargingState(playerBall));
        }
    }

    public void Exit()
    {
        playerBall.charge = 0;
    }
}

public class ChargingState : IState
{
    private playerBall playerBall;

    public ChargingState(playerBall playerBall) { this.playerBall = playerBall; }

    public void Enter()
    {
        return;
    }

    public void Tick()
    {
        playerBall.ChargeShot();
        if (playerBall.inputShoot.WasPressedThisFrame())
        {
            playerBall.stateMachine.ChangeState(new FiredState(playerBall));
        }
    }

    public void Exit()
    {
        playerBall.reticle.SetActive(false);
    }
}

public class FiredState : IState
{
    private playerBall playerBall;

    private float timer = 0;

    public FiredState(playerBall playerBall) { this.playerBall = playerBall; }

    public void Enter()
    {
        playerBall.ShootBall();
        timer = 2.1f;
    }

    public void Tick()
    {
        if (playerBall.rb2d.velocity.x == 0f && playerBall.rb2d.velocity.y == 0)
        {
            timer -= 1 * Time.deltaTime;
            Debug.Log("fired timer");
        }

        if (timer <= 0)
        {
            playerBall.EventBallStopped.Invoke();
            // if (piso esta bien) { nuevo lastPosition} si no { mover a last position}
        }
        
    }

    public void Exit()
    {
        return;
    }
}
