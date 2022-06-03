using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CaptainLogic : GameLogic
{
    private bool playing = false;

    [SerializeField] private Transform gameCamera;

    private List<GameObject> players;
    private int currentPlayer;
    private int numPlayers;

    private List<int> scores;
    private List<TextMeshProUGUI> playerScores;
    private List<TextMeshProUGUI> roundScores;
    private float currentScore = 0;

    private TimeSpan gameTimer;
    public TextMeshProUGUI timerText;

    private float Timer = 0;

    void Start()
    {
        if (!GameStatus.instance.playing) {
            GameStatus.instance.startMiniGame(4, SceneManager.GetActiveScene().name, false);
        }

        numPlayers = GameStatus.instance.playerCount;
        InitializePlayers();
        UpdateCamera();
        ResetTimer();

        gameTimer = TimeSpan.FromSeconds(10);
        playing = true;
    }

    void InitializePlayers() 
    {
        Transform allPlayers = GameObject.Find("Players").transform;
        RectTransform allScores = GameObject.Find("Scores").GetComponent<RectTransform>();

        players = new List<GameObject>();
        playerScores = new List<TextMeshProUGUI>();
        roundScores = new List<TextMeshProUGUI>();
        scores = new List<int>(new int[numPlayers]);

        // Set active only the correct number of players
        for (int i = 0; i < 4; i++) {
            if (i < numPlayers) {
                players.Add(allPlayers.GetChild(i).gameObject);
                playerScores.Add(allScores.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>());
                roundScores.Add(allScores.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>());
            } else {
                allPlayers.GetChild(i).gameObject.SetActive(false);
                allScores.GetChild(i).gameObject.SetActive(false);
            }
        }

        currentPlayer = 0;

        players[currentPlayer].GetComponent<PlayerMovement>().inFront = true;

        players[0].GetComponent<PlayerMovement>().playerInFront = players[numPlayers - 1].transform;

        for (int i = 1; i < numPlayers; i++) {
            players[i].GetComponent<PlayerMovement>().playerInFront = players[i - 1].transform;
        }
    }

    void UpdateCamera()
    {
        gameCamera.GetComponent<CameraFollow>().target = players[currentPlayer].transform;
    }

    void UpdatePlayer()
    {
        currentScore = 0;
        roundScores[currentPlayer].text = currentScore.ToString();

        players[currentPlayer].GetComponent<PlayerMovement>().inFront = false;
        currentPlayer = (currentPlayer + 1) % numPlayers;
        players[currentPlayer].GetComponent<PlayerMovement>().inFront = true;
    }

    void ResetTimer()
    {
        Timer = UnityEngine.Random.Range(10, 20);
    }

    void Update()
    {
        if (!playing) return;

        gameTimer -= TimeSpan.FromSeconds(Time.deltaTime);
        Timer -= Time.deltaTime;

        if (gameTimer <= TimeSpan.Zero) {
            playing = false;
            GameStatus.instance.finishMiniGame(scores);
            return;
        } else {
            timerText.text = "Time\n" + gameTimer.ToString("mm':'ss");
        }

        if (Timer <= 0) {
            UpdatePlayer();
            UpdateCamera();
            ResetTimer();
        } else {
            currentScore += 100 * Time.deltaTime;
            roundScores[currentPlayer].text = ((int)currentScore).ToString();
        }
    }
    
    public override void playerAction(GameObject player)
    {
        if (players[currentPlayer] == player) {
            scores[currentPlayer] += (int) currentScore;
            playerScores[currentPlayer].text = (scores[currentPlayer]).ToString();
            UpdatePlayer();
            UpdateCamera();
        }
    }
}
