using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [Header("Properties")]
    public int playerCount = 1;
    public GameObject playerPrefab;

    [Header("Level Info")]
    public Vector2 levelStartPos;
    public int levelPar;
    public int currentLevel;

    [Header("Additional Scripts")]
    public turnSystem turnSystem;
    public cameraController cameraController;

    public void Awake()
    {
        turnSystem.allPlayersDone.AddListener(DeclareWinner);
        turnSystem.nextPlayer.AddListener(cameraController.Changetarget);
    }

    public void Start()
    {
        ChangeLevel(1);
        CreatePlayers(playerCount);
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
        Debug.Log("the winner is player " + winner.playerNmr +"!");
    }

    private void ChangeLevel(int levelNmr)
    {
        Object prefab;
        prefab = Resources.Load("levels/level" + levelNmr);
        levelInfo newLevelInfo = prefab.GetComponent<levelInfo>();
        Instantiate(prefab, newLevelInfo.instancePos, new Quaternion());
        turnSystem.startPos = newLevelInfo.startArea.transform.position + newLevelInfo.instancePos;
        levelPar = newLevelInfo.par;
        currentLevel = levelNmr;
    }
}
