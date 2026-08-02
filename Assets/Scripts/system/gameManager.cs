using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [Header("Properties")]
    public int playerCount = 1;
    public GameObject playerPrefab;

    [Header("Level Info")]
    public Vector2 levelStartPos;

    [Header("Additional Scripts")]
    public turnSystem turnSystem;

    public void Awake()
    {
        turnSystem.allPlayersDone.AddListener(DeclareWinner);
    }

    public void Start()
    {
        CreatePlayers(playerCount);
        turnSystem.startPos = levelStartPos;
        turnSystem.GoToNextPlayer();

        
    }

    public void CreateAPlayer(int playerNmr)
    {
        GameObject newPlayer = Instantiate(playerPrefab);
        newPlayer.GetComponent<playerBall>().playerNmr = playerNmr;
        newPlayer.GetComponent<playerBall>().EventBallStopped.AddListener(turnSystem.GoToNextPlayer);
        newPlayer.GetComponent<playerBall>().stateMachine.ChangeState(new TurnoffState(newPlayer.GetComponent<playerBall>()));
        turnSystem.playerList.Add(newPlayer);
    }

    public void CreatePlayers(int playerCount)
    {
        for (var i = 1; i < playerCount + 1; i++)
        {
            CreateAPlayer(i);
        }
    }

    public void DeclareWinner()
    {
        int mostShots = 99;
        playerBall winner = turnSystem.playerList[0].GetComponent<playerBall>(); //agarro el componente para que debug log no tenga error (antes esta vacio antes)
        foreach (GameObject i in turnSystem.playerList)
        {
            playerBall player = i.GetComponent<playerBall>();
            if (player.totalShots > mostShots)
            {
                mostShots = player.totalShots;
                winner = player;
            }
        }
        Debug.Log("the winner is" + winner.playerNmr);
    }
}
