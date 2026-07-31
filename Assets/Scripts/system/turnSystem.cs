using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnSystem : MonoBehaviour
{
    [Header("Properties")]
    public List<GameObject> playerList = new List<GameObject>();
    public int currentPlayerTurn;

    public void GoToNextPlayer()
    {
        playerBall currentPlayer;
        playerBall nextPlayer;
        currentPlayer = playerList[currentPlayerTurn - 1].GetComponent<playerBall>(); //resto 1 al int porque el index de una lista empieza en 0
        currentPlayer.stateMachine.ChangeState(new TurnoffState(currentPlayer));
        currentPlayerTurn += 1;
        if (currentPlayerTurn > playerList.Count) { currentPlayerTurn = 1; }
        for (int i = currentPlayerTurn ; !playerList[currentPlayerTurn - 1].activeSelf; i++) //va la siguente jugador que esta activo
        {
            currentPlayerTurn = i;
            if (i + 1 > playerList.Count) { i = 0; } //evita que la condicion de la iteracion de error
        }
        nextPlayer = playerList[currentPlayerTurn - 1].GetComponent<playerBall>();
        nextPlayer.stateMachine.ChangeState(new AimingState(nextPlayer));
    }

    public void ResetTurnOrder()
    {
        playerBall player;
        currentPlayerTurn = 1;
        player = playerList[currentPlayerTurn - 1].GetComponent<playerBall>();
        player.stateMachine.ChangeState(new AimingState(player));
    }
}
