using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RythmLogic : GameLogic
{
    public bool playing = false;

    [SerializeField] private TextMeshProUGUI countDown;
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

        gameTimer = TimeSpan.FromSeconds(60);
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
                scoreTexts.Add(allScores.GetChild(i).GetComponent<TextMeshProUGUI>());
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
            StartCoroutine(finishMiniGame());
            return;
        } else {
            timerText.text = "TIME\n" + gameTimer.ToString("mm':'ss");
        }
    }

    public override void playerAction(GameObject player){
        if(playing){
            Animator animator = player.GetComponent<Animator>();
            animator.Play("RedNote");
            checkBallHit(player, "red");
            StartCoroutine(returnToNormalState(animator));
        }
            
    }

    public override void playerBlue(GameObject player){
        if(playing){
            Animator animator = player.GetComponent<Animator>();
            animator.Play("BlueNote");
            checkBallHit(player, "blue");
            StartCoroutine(returnToNormalState(animator));
        }
            
    }

    public override void playerOrange(GameObject player){
        if(playing){
            Animator animator = player.GetComponent<Animator>();
            animator.Play("OrangeNote");
            checkBallHit(player, "orange");
            StartCoroutine(returnToNormalState(animator));
        }
    }

    public override void playerGreen(GameObject player){
        if(playing){
            Animator animator = player.GetComponent<Animator>();
            animator.Play("GreenNote");
            checkBallHit(player, "green");
            StartCoroutine(returnToNormalState(animator));
        }
    }

    public override void playerYellow(GameObject player){
        if(playing){
            Animator animator = player.GetComponent<Animator>();
            animator.Play("YellowNote");
            checkBallHit(player, "yellow");
            StartCoroutine(returnToNormalState(animator));
        }
    }

    private void checkBallHit(GameObject player, string button)
    {

        NoteObject ball = player.GetComponent<ButtonController>().ball;

        if (ball == null) {
            updatePlayerScore(player, -10);
            return;
        }

        if (ball.color != button) {
            return;
        }

        playerHit(player, ball);
    }

    private void playerHit(GameObject player, NoteObject ball)
    {
        float height = ball.getHeight();
        Destroy(ball.gameObject);

        int score;

        if (Math.Abs(height) > 0.25f){
            print("Hit!");
            score = 5;
        } 
        else if(Math.Abs(height) > 0.05f){
            print("Great!");
            score = 25;
        } 
        else{
            print("Perfect!");
            score = 45;
        } 

        updatePlayerScore(player, score);
    }

    private void updatePlayerScore(GameObject player, int score)
    {
        int index = player.GetComponent<InputController>().playerNumber - 1;
        playerScores[index] += score;
        scoreTexts[index].text = playerScores[index].ToString();
    }

    IEnumerator finishMiniGame()
    {
        playing = false;
        timerText.text = "FINISHED";

        yield return new WaitForSeconds(3.0f);

        GameStatus.instance.finishMiniGame(playerScores);
    }

    IEnumerator returnToNormalState(Animator player){
        yield return new WaitForSeconds(0.4f);
        player.Play("Default");
    }
}
