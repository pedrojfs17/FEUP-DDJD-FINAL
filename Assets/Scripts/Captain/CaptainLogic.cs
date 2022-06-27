using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
using TMPro;

public class CaptainLogic : GameLogic
{
    public bool playing = false;
    private bool moving = false;

    [SerializeField] private Transform gameCamera;
    [SerializeField] private FMODUnity.StudioEventEmitter gunShot;
    [SerializeField] private ParticleSystem hitFx;

    private List<GameObject> players;
    private int currentPlayer;
    private int numPlayers;

    private List<int> scores;
    private List<TextMeshProUGUI> playerScores;

    [SerializeField] private TextMeshProUGUI accumulator;
    [SerializeField] private TextMeshProUGUI countDown;
    [SerializeField] private TextMeshProUGUI timerText;

    private float currentScore = 0;

    private TimeSpan gameTimer;

    private float Timer = 0;
    private float turnDelay;

    void Start()
    {
        if (!GameStatus.instance.playing) {
            GameStatus.instance.startMiniGame(4, SceneManager.GetActiveScene().name, false);
        }

        numPlayers = GameStatus.instance.playerCount;
        turnDelay = 0.4f + numPlayers * 0.1f;
        InitializePlayers();
        UpdateCamera();

        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        WaitForSeconds waitHalfSecond = new WaitForSeconds(0.5f);
        WaitForSeconds waitOneSecond = new WaitForSeconds(1);

        yield return waitOneSecond;
        yield return waitOneSecond;

        for (int i = 3; i > 0; i--) {
            countDown.text = i.ToString();
            yield return waitOneSecond;
        }

        countDown.text = "START";
        yield return waitHalfSecond;
        countDown.text = "";

        ResetTimer();
        gameTimer = TimeSpan.FromSeconds(20);
        playing = true;
    }

    void InitializePlayers() 
    {
        Transform allPlayers = GameObject.Find("Players").transform;
        RectTransform allScores = GameObject.Find("Scores").GetComponent<RectTransform>();

        players = new List<GameObject>();
        playerScores = new List<TextMeshProUGUI>();
        scores = new List<int>(new int[numPlayers]);

        // Set active only the correct number of players
        for (int i = 0; i < 4; i++) {
            if (i < numPlayers) {
                players.Add(allPlayers.GetChild(i).gameObject);
                playerScores.Add(allScores.GetChild(i).GetComponent<TextMeshProUGUI>());
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

        moving = true;
        players[currentPlayer].GetComponent<PlayerMovement>().inFront = false;
        currentPlayer = (currentPlayer + 1) % numPlayers;
        players[currentPlayer].GetComponent<PlayerMovement>().inFront = true;

        StartCoroutine(endMovement());
    }

    IEnumerator endMovement()
    {
        yield return new WaitForSeconds(turnDelay);
        moving = false;
    }

    void FinishMovement() {
        moving = false;
    }

    void ResetTimer()
    {
        Timer = UnityEngine.Random.Range(5f, 10f);
    }

    public Vector3 GetFirstPlayerPosition()
    {
        return players[currentPlayer].transform.position;
    }

    void Update()
    {
        if (!playing) return;

        gameTimer -= TimeSpan.FromSeconds(Time.deltaTime);
        Timer -= Time.deltaTime;

        if (gameTimer <= TimeSpan.Zero) {
            StartCoroutine(finishMiniGame());
            return;
        } else {
            timerText.text = "TIME\n" + gameTimer.ToString("mm':'ss");
        }

        if (Timer <= 0) {
            GunShot();
        } else {
            currentScore += 50 * Time.deltaTime;
            accumulator.text = ((int)currentScore).ToString();
        }
    }

    void GunShot()
    {
        ResetTimer();
        gunShot.Play();

        StartCoroutine(hitPlayer());
    }

    IEnumerator hitPlayer()
    {
        yield return new WaitForSeconds(0.4f);

        hitFx.transform.position = players[currentPlayer].transform.position + new Vector3(0.75f, 1.25f, 0);
        hitFx.Play();
        hitFx.GetComponent<FMODUnity.StudioEventEmitter>().Play();

        UpdatePlayer();
        UpdateCamera();
    }
    
    public override void playerAction(GameObject player)
    {
        if (playing && !moving && players[currentPlayer] == player) {
            scores[currentPlayer] += (int) currentScore;
            playerScores[currentPlayer].text = (scores[currentPlayer]).ToString();
            UpdatePlayer();
            UpdateCamera();
        }
    }

    IEnumerator finishMiniGame()
    {
        playing = false;
        timerText.text = "FINISHED";

        yield return new WaitForSeconds(1.5f);

        GameStatus.instance.finishMiniGame(scores);
    }
}
