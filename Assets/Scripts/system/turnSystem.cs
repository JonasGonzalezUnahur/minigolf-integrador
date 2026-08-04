using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class turnSystem : MonoBehaviour
{
    [Header("Properties")]
    public List<GameObject> playerList = new List<GameObject>();
    public int currentPlayerTurn;
    public bool firstTurn = true;

    [Header("Level Info")]
    public Vector2 startPos;

    [Header("Events")]
    public UnityEvent allPlayersDone;
    public UnityEvent<GameObject> nextPlayer;

    public void GoToNextPlayer()
    {
        playerBall currentPlayer;
        playerBall nextPlayer;
        if (!firstTurn)
        {
            currentPlayer = playerList[currentPlayerTurn - 1].GetComponent<playerBall>(); //resto 1 al int porque el index de una lista empieza en 0
            currentPlayer.stateMachine.ChangeState(new TurnoffState(currentPlayer));
        } else { firstTurn = false; }
        if (playerList.Exists(x => !x.GetComponent<playerBall>().finished)) //al menos un jugador no termino
        {
            currentPlayerTurn += 1;
            if (currentPlayerTurn > playerList.Count) { currentPlayerTurn = 1; }
            for (int i = currentPlayerTurn; playerList[Mathf.Max(0, currentPlayerTurn - 1)].GetComponent<playerBall>().finished; i++) //va la siguente jugador que esta activo
            {
                currentPlayerTurn = i;
                if (i + 1 > playerList.Count) { i = 0; } //evita que la condicion de la iteracion de error
            }
            nextPlayer = playerList[currentPlayerTurn - 1].GetComponent<playerBall>();
            if (!nextPlayer.inPlay) { nextPlayer.EnterPlay(startPos); nextPlayer.lastPosition = startPos; }
            nextPlayer.stateMachine.ChangeState(new AimingState(nextPlayer));
            this.nextPlayer.Invoke(playerList[Mathf.Max(0, currentPlayerTurn - 1)]);
        }
        else
        {
            allPlayersDone.Invoke();
        }
    }
}
