using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    public bool playing { get; private set; }
    public int miniGameCount { get; private set; }
    public int playerCount { get; private set; }

    private List<string> miniGames;
    private Queue<string> gamesToPlay;

    private List<int> playerScores;

    // Instance
    public static GameStatus instance;

    void Awake() {
        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings; 

        miniGames = new List<string>();

        for (int i = 0; i < sceneCount; i++) {
            string path = SceneUtility.GetScenePathByBuildIndex(i);

            if (Regex.IsMatch(path, @"^Assets/Scenes/MiniGames/.+\.unity$")) {
                path = Regex.Replace(path, @"^Assets/Scenes/MiniGames/", "");
                path = Regex.Replace(path, @".unity$", "");
                miniGames.Add(path);
            }
        }

        miniGameCount = miniGames.Count;
    }

    private void prepareGames(int nMiniGames)
    {
        List<string> availableGames = new List<string>(miniGames);

        gamesToPlay = new Queue<string>();

        while (nMiniGames > 0) {
            int randomNumber = UnityEngine.Random.Range(0, availableGames.Count);

            gamesToPlay.Enqueue(availableGames[randomNumber]);

            availableGames.RemoveAt(randomNumber);

            nMiniGames--;
        }
    }

    private void loadNextMiniGame()
    {
        if (gamesToPlay.Count > 0)
            SceneManager.LoadScene(gamesToPlay.Dequeue());
        else
            SceneManager.LoadScene("Menu");
    }

    private void initializePlayers(int nPlayers)
    {
        playerCount = nPlayers;
        playerScores = new List<int>(new int[playerCount]);
    }

    public void startMiniGame(int nPlayers, string miniGame, bool loadScene = true)
    {
        initializePlayers(nPlayers);

        playing = true;
        
        gamesToPlay = new Queue<string>();

        if (loadScene) {
            gamesToPlay.Enqueue(miniGame);
            loadNextMiniGame();
        }
    }

    public void startFullGame(int nPlayers, int nMiniGames)
    {
        // Randomize Mini-Games to play
        prepareGames(nMiniGames);

        // Initialize player scores
        initializePlayers(nPlayers);

        playing = true;

        // load Game
        loadNextMiniGame();
    }

    private List<int> pointsFromScore(List<int> playerScores)
    {
        List<int> points = new List<int>(new int[playerCount]);

        List<int> distinctScores = playerScores.Distinct().ToList();
        distinctScores.Sort();

        for (int i = 0; i < playerCount; i++) {
            points[i] = distinctScores.IndexOf(playerScores[i]);
        }

        return points;
    }

    public void finishMiniGame(List<int> gameScores)
    {
        List<int> playerPoints = pointsFromScore(gameScores);

        // Give Player points
        for (int i = 0; i < playerCount; i++) {
            playerScores[i] += playerPoints[i];
            print("Player " + (i + 1).ToString() + " has " + playerScores[i].ToString() + " points!");
        }

        // Load Next Game
        loadNextMiniGame();
    }
}
