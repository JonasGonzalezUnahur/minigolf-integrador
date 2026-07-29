using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [Header("Properties")]
    public int playerCount = 1;
    public GameObject playerPrefab;

    [Header("Additional Scripts")]
    public turnSystem turnSystem;

    public void Start()
    {
        CreatePlayers(playerCount);
        turnSystem.ResetTurnOrder();
    }

    public void CreateAPlayer(int playerNmr)
    {
        GameObject newPlayer = Instantiate(playerPrefab);
        newPlayer.GetComponent<playerBall>().playerNmr = playerNmr;
        newPlayer.GetComponent<playerBall>().EventBallStopped.AddListener(turnSystem.GoToNextPlayer);
        newPlayer.GetComponent<playerBall>().stateMachine.ChangeState(new TurnoffState(newPlayer.GetComponent<playerBall>()));
        turnSystem.playerList.Add(newPlayer);
        newPlayer.transform.position = new Vector3(2 * playerNmr, 2 * playerNmr);
    }

    public void CreatePlayers(int playerCount)
    {
        for (var i = 1; i < playerCount + 1; i++)
        {
            CreateAPlayer(i);
        }
    }
}
