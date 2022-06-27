using System;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsLogic : GameLogic
{
    private int numPlayers;
    private int playersReady;

    private List<Animator> playersAnimations;
    private List<bool> playersReadyStatus;

    private void Start() {
        string currentGame = GameStatus.instance.currentGame;
        Transform gameInstructions = GameObject.Find("GameInstructions").transform;

        foreach (Transform game in gameInstructions) {
            if (game.name == currentGame) {
                game.gameObject.SetActive(true);
                break;
            }
        }

        numPlayers = GameStatus.instance.playerCount;
        playersReady = 0;

        Transform allPlayers = GameObject.Find("Players").transform;
        RectTransform allPlayerStatus = GameObject.Find("Ready").GetComponent<RectTransform>();

        playersAnimations = new List<Animator>();
        playersReadyStatus = new List<bool>();

        for (int i = 0; i < 4; i++) {
            if (i < numPlayers) {
                playersAnimations.Add(allPlayerStatus.GetChild(i).GetComponent<Animator>());
                playersReadyStatus.Add(false);
            } else {
                allPlayers.GetChild(i).gameObject.SetActive(false);
                allPlayerStatus.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public override void playerAction(GameObject player)
    {
        int playerIndex = player.GetComponent<InputController>().playerNumber - 1;

        if (playersReadyStatus[playerIndex])
            setPlayerNotReady(playerIndex);
        else
            setPlayerReady(playerIndex);
    }

    private void setPlayerReady(int index)
    {
        playersAnimations[index].Play("Ready");
        playersReadyStatus[index] = true;
        playersReady += 1;

        if (playersReady == numPlayers) {
            GameStatus.instance.finishInstructions();
        }
    }

    private void setPlayerNotReady(int index)
    {
        playersAnimations[index].Play("NotReady");
        playersReadyStatus[index] = false;
        playersReady -= 1;
    }
}
