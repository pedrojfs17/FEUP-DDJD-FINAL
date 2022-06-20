using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RythmLogic : GameLogic
{
    private bool playing = false;

    public TextMeshProUGUI timerText;
    private TimeSpan gameTimer;

    private List<int> playerScores;
    private List<TextMeshProUGUI> scoreTexts;

    private int numPlayers;

    void Start()
    {
        if (!GameStatus.instance.playing) {
            GameStatus.instance.startMiniGame(4, SceneManager.GetActiveScene().name, false);
        }

        numPlayers = GameStatus.instance.playerCount;
        InitializePlayers();

        gameTimer = TimeSpan.FromSeconds(20);
        playing = true;
    }

    void InitializePlayers() 
    {
        Transform allPlayers = GameObject.Find("Players").transform;
        Transform allBeats = GameObject.Find("Beats").transform;
        Transform allSpawners = GameObject.Find("Spawners").transform;
        RectTransform allScores = GameObject.Find("Scores").GetComponent<RectTransform>();

        scoreTexts = new List<TextMeshProUGUI>();
        playerScores = new List<int>(new int[numPlayers]);

        // Set active only the correct number of players
        for (int i = 0; i < 4; i++) {
            if (i < numPlayers) {
                scoreTexts.Add(allScores.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>());
            } else {
                allPlayers.GetChild(i).gameObject.SetActive(false);
                allBeats.GetChild(i).gameObject.SetActive(false);
                allSpawners.GetChild(i).gameObject.SetActive(false);
                allScores.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (!playing) return;

        gameTimer -= TimeSpan.FromSeconds(Time.deltaTime);

        if (gameTimer <= TimeSpan.Zero) {
            playing = false;
            GameStatus.instance.finishMiniGame(playerScores);
        } else {
            timerText.text = "Time\n" + gameTimer.ToString("mm':'ss");
        }
    }

    public override void playerAction(GameObject player){
        int score = 5;

        NoteObject ball = player.GetComponent<ButtonController>().hitBall();

        if (ball == null) {
            updatePlayerScore(player, -10);
            return;
        }

        float height = ball.getHeight();
        Destroy(ball.gameObject);

        if (Math.Abs(height) > 0.25f){
            print("Hit!");
            score += 5;
        } 
        else if(Math.Abs(height) > 0.05f){
            print("Great!");
            score += 25;
        } 
        else{
            print("Perfect!");
            score += 45;
        } 

        updatePlayerScore(player, score);
    }

    private void updatePlayerScore(GameObject player, int score)
    {
        int index = player.GetComponent<InputController>().playerNumber - 1;
        playerScores[index] += score;
        scoreTexts[index].text = playerScores[index].ToString();
    }
}
