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
    public Object levelPrefab;

    [Header("Additional Scripts")]
    public turnSystem turnSystem;
    public cameraController cameraController;

    public void Awake()
    {
        turnSystem.allPlayersDone.AddListener(GoToNextLevel);
        turnSystem.nextPlayer.AddListener(cameraController.Changetarget);
    }

    public void Start()
    {
        CreatePlayers(playerCount);
        ChangeLevel(1);
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
        GameObject text = new GameObject("winnertext");
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
        var textMesh = text.AddComponent<TextMesh>();
        textMesh.text = "the winner is player " + winner.playerNmr + "!";
        textMesh.fontSize = 64;
        textMesh.characterSize = 0.1f;
        textMesh.anchor = TextAnchor.LowerCenter;
        textMesh.alignment = TextAlignment.Center;
        text.transform.position = cameraController.transform.position;
    }

    private void ChangeLevel(int levelNmr)
    {
        GameObject prefab = Instantiate(Resources.Load<GameObject>("levels/level" + levelNmr)); ;
        levelInfo newLevelInfo = prefab.GetComponent<levelInfo>();
        levelPrefab = prefab;
        turnSystem.startPos = newLevelInfo.startArea.transform.position;
        levelPar = newLevelInfo.par;
        currentLevel = levelNmr;
        foreach (GameObject i in turnSystem.playerList)
        {
            playerBall player = i.GetComponent<playerBall>();
            player.shots = 0;
            player.shotsLimit = newLevelInfo.par * 2;
            
        }
    }

    private void GoToNextLevel()
    {
        if (currentLevel == 2)
        {
            DeclareWinner();
        } else
        {
            Destroy(levelPrefab);
            ChangeLevel(currentLevel + 1);
            turnSystem.ResetTurnOrder();
        }
    }
}
