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

    public List<int> playerScores { get; private set; }
    public List<int> playerPositions { get; private set; }

    // Instance
    public static GameStatus instance;
    public static List<string> ColorMapping = new List<string>(){ "Blue", "Orange", "Green", "Yellow" };

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

    private void loadNextMiniGame(bool loadCutscene = false)
    {
        LevelLoader levelLoader = GameObject.FindObjectOfType<LevelLoader>();

        if (loadCutscene)
            levelLoader.LoadScene("Cutscene");
        else {
            if (gamesToPlay.Count > 0)
                levelLoader.LoadScene(gamesToPlay.Dequeue());
            else
                levelLoader.LoadScene("FinalCutscene");
        }

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

    private List<int> pointsFromPositions(List<int> playerPositions)
    {
        List<int> points = new List<int>(new int[playerCount]);

        List<int> POSITION_POINTS = new List<int> { 5, 3, 1, 0 };

        for (int i = 0; i < playerCount; i++) {
            points[i] = POSITION_POINTS[playerPositions[i] - 1];
        }

        return points;
    }

    private List<int> getPositions(List<int> playerScores)
    {
        List<int> positions = new List<int>(new int[playerCount]);

        List<int> distinctScores = playerScores.ToList();
        distinctScores.Sort();
        distinctScores.Reverse();

        for (int i = 0; i < playerCount; i++) {
            positions[i] = distinctScores.IndexOf(playerScores[i]) + 1;
        }

        return positions;
    }

    public void finishMiniGame(List<int> gameScores)
    {
        playerPositions = getPositions(gameScores);

        // Load Cutscene
        loadNextMiniGame(true);
    }

    public void finishCutscene()
    {
        List<int> playerPoints = pointsFromPositions(playerPositions);

        // Give Player points
        for (int i = 0; i < playerCount; i++) {
            playerScores[i] += playerPoints[i];
            print("Player " + (i + 1).ToString() + " has " + playerScores[i].ToString() + " points!");
        }

        // Load Next Game
        loadNextMiniGame();
    }

    public void finishGame()
    {
        LevelLoader levelLoader = GameObject.FindObjectOfType<LevelLoader>();

        levelLoader.LoadScene("Menu");
    }
}
