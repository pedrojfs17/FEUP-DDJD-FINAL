using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SteelHeadLogic : GameLogic
{
    private bool playing = false;

    private Queue<Tuple<int, int>> rounds;
    private float roundStatus;
    private int currentRoundPoints;
    private int? previousLoser;
    private int? previousWinner;

    private int numPlayers;
    private List<MoveToTarget> players;

    private List<int> scores;
    private List<TextMeshProUGUI> playerScoreTexts;

    [SerializeField] private List<Transform> defaultPositions;
    [SerializeField] private Transform defaultParent;
    [SerializeField] private Transform duelParent;
    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform rightPosition;

    [SerializeField] private TextMeshProUGUI countDown;

    private void Start()
    {
        if (!GameStatus.instance.playing) {
            GameStatus.instance.startMiniGame(4, SceneManager.GetActiveScene().name, false);
        }

        numPlayers = GameStatus.instance.playerCount;

        InitializePlayers();
        PrepareRounds();

        StartCoroutine(WaitBeforeRound());
    }

    private void InitializePlayers() 
    {
        Transform allPlayers = GameObject.Find("Players").transform;
        RectTransform allScores = GameObject.Find("Scores").GetComponent<RectTransform>();

        players = new List<MoveToTarget>();
        playerScoreTexts = new List<TextMeshProUGUI>();
        scores = new List<int>(new int[numPlayers]);

        // Set active only the correct number of players
        for (int i = 0; i < 4; i++) {
            if (i < numPlayers) {
                players.Add(allPlayers.GetChild(i).GetComponent<MoveToTarget>());
                playerScoreTexts.Add(allScores.GetChild(i).GetComponent<TextMeshProUGUI>());
            } else {
                allPlayers.GetChild(i).gameObject.SetActive(false);
                allScores.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void PrepareRounds()
    {
        List<int> remainingPlayers = new List<int>(){ 0, 1, 2, 3 };

        // Randomize List
        for (int t = 0; t < remainingPlayers.Count; t++)
        {
            int tmp = remainingPlayers[t];
            int r = UnityEngine.Random.Range(t, remainingPlayers.Count);
            remainingPlayers[t] = remainingPlayers[r];
            remainingPlayers[r] = tmp;
        }

        rounds = new Queue<Tuple<int, int>>();

        AddRound(remainingPlayers[0], remainingPlayers[1]);
        AddRound(remainingPlayers[2], remainingPlayers[3]);

        currentRoundPoints = 3;
        previousLoser = null;
        previousWinner = null;
    }

    private void AddRound(int player1, int player2)
    {
        int leftPlayer = player1 < player2 ? player1 : player2;
        int rightPlayer = player1 < player2 ? player2 : player1;

        rounds.Enqueue(new Tuple<int, int>(leftPlayer, rightPlayer));
    }

    private void Update() 
    {
        if (!playing) return;

        duelParent.Translate(roundStatus * Time.deltaTime, 0, 0);

        if (Math.Abs(duelParent.position.x) > 2.5) {
            Tuple<int, int> roundPlayers = rounds.Dequeue();
            if (duelParent.position.x > 0) {
                EndRound(roundPlayers.Item1, roundPlayers.Item2);
            } else {
                EndRound(roundPlayers.Item2, roundPlayers.Item1);
            }
        }
    }

    private void EndRound(int roundWinner, int roundLoser)
    {
        playing = false;

        scores[roundWinner] += currentRoundPoints;
        playerScoreTexts[roundWinner].text += "W";
        playerScoreTexts[roundLoser].text += "L";
        
        PutPlayerInPosition(roundWinner, defaultPositions[roundWinner], defaultParent, true);
        PutPlayerInPosition(roundLoser, defaultPositions[roundLoser], defaultParent, true);
       
        if (rounds.Count > 0) {
            previousLoser = roundLoser;
            previousWinner = roundWinner;
        } else if (currentRoundPoints == 1) {
            StartCoroutine(FinishMiniGame());
            return;
        } else {
            AddRound(previousLoser.Value, roundLoser);
            AddRound(previousWinner.Value, roundWinner);
            currentRoundPoints = 1;
        }

        StartCoroutine(WaitBeforeRound());
    }

    private void PutPlayerInPosition(int index, Transform position, Transform parent, bool startByRotating)
    {
        players[index].SetTarget(position, startByRotating);
        players[index].transform.SetParent(parent);
    }

    IEnumerator WaitBeforeRound()
    {
        yield return new WaitForSeconds(2f);

        StartCoroutine(StartRound());
    }

    IEnumerator StartRound()
    {
        duelParent.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        Tuple<int, int> roundPlayers = rounds.Peek();
        PutPlayerInPosition(roundPlayers.Item1, leftPosition, duelParent, false);
        PutPlayerInPosition(roundPlayers.Item2, rightPosition, duelParent, false);

        roundStatus = 0;

        yield return new WaitForSeconds(1f);

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
        
        playing = true;
    }

    IEnumerator FinishMiniGame()
    {
        yield return new WaitForSeconds(2.5f);

        GameStatus.instance.finishMiniGame(scores);
    }

    public override void playerAction(GameObject player)
    {
        if (!playing) return;

        int playerIndex = player.GetComponent<InputController>().playerNumber - 1;
        Tuple<int, int> roundPlayers = rounds.Peek();

        if (roundPlayers.Item1 == playerIndex)
            roundStatus += 0.2f;
        else if (roundPlayers.Item2 == playerIndex) 
            roundStatus -= 0.2f;
    }
}
