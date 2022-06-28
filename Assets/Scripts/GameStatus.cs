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
    private Queue<string> scenesToPlay;

    public List<int> playerScores { get; private set; }
    public List<int> playerPositions { get; private set; }
    public List<int> lastGamePositions { get; private set; }

    public string currentGame;

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

        scenesToPlay = new Queue<string>();

        while (nMiniGames > 0 && availableGames.Count > 0) {
            int randomNumber = UnityEngine.Random.Range(0, availableGames.Count);

            scenesToPlay.Enqueue("Instructions");
            scenesToPlay.Enqueue(availableGames[randomNumber]);
            scenesToPlay.Enqueue("Cutscene");

            availableGames.RemoveAt(randomNumber);

            nMiniGames--;
        }
    }

    private void loadNextScene()
    {
        LevelLoader levelLoader = GameObject.FindObjectOfType<LevelLoader>();

        if (scenesToPlay.Count > 0) {
            string sceneToLoad = scenesToPlay.Dequeue();

            if (sceneToLoad == "Instructions")
                currentGame = scenesToPlay.Peek();

            levelLoader.LoadScene(sceneToLoad);
        }
        else
            levelLoader.LoadScene("FinalCutscene");
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
        
        scenesToPlay = new Queue<string>();

        if (loadScene) {
            scenesToPlay.Enqueue("Instructions");
            scenesToPlay.Enqueue(miniGame);
        }

        scenesToPlay.Enqueue("Cutscene");

        if (loadScene)
            loadNextScene();
    }

    public void startFullGame(int nPlayers, int nMiniGames)
    {
        // Randomize Mini-Games to play
        prepareGames(nMiniGames);

        // Initialize player scores
        initializePlayers(nPlayers);

        playing = true;

        // load Game
        loadNextScene();
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
        distinctScores = distinctScores.Distinct().ToList();
        distinctScores.Sort();
        distinctScores.Reverse();

        for (int i = 0; i < playerCount; i++) {
            int position = distinctScores.IndexOf(playerScores[i]) + 1;
            positions[i] = (position == distinctScores.Count) ? 4 : position;
        }

        return positions;
    }
    
    public void finishInstructions()
    {
        loadNextScene();
    }

    public void finishMiniGame(List<int> gameScores)
    {
        lastGamePositions = getPositions(gameScores);

        loadNextScene();
    }

    public void finishCutscene()
    {
        List<int> playerPoints = pointsFromPositions(lastGamePositions);

        // Give Player points
        for (int i = 0; i < playerCount; i++) {
            playerScores[i] += playerPoints[i];
        }
        
        playerPositions = getPositions(playerScores);

        // Load Next Game
        loadNextScene();
    }

    public void finishGame()
    {
        LevelLoader levelLoader = GameObject.FindObjectOfType<LevelLoader>();

        levelLoader.LoadScene("Menu");
    }
}
