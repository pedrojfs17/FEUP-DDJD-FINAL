# Integration of New Games

In order to integrate new games, the following must be assured.

## Players

Each game should have a GameObject called `Players`, with the 4 players inside, with the names as in the next example.

```
Players
- Player1
- Player2
- Player3
- Player4
```

Each of the players should have a component with the [Input Controller](#input-controller). 

## Input Controller

This script is responsible for enabling the correct keys for each player and for triggering the possible game actions. 
It enables the correct commands based on the player name, so it it very important to keep the naming as `PlayerX` where `X` is the player number.
Whenever a player clicks a button, a funcion will be called on the [Game Logic](#game-logic) class, explained in the next chapter.

## Game Logic

Every game should have an object called `GameLogic`. This object should have a script with all the game logic inside it called `XLogic` where `X` is the name of the game (not necessary to be called with the name of the game, but as there will be one of these GameLogic scripts for each game, it is convenient to name them accordingly). This script needs to extend the `GameLogic` class. This class is a normal Unity object but comes with the following virtual methods, that should be used to handle the player input:

```cs
public virtual void playerAction(GameObject player) {}  // player clicked the red button 
public virtual void playerBlue(GameObject player) {}    // player clicked the blue button
public virtual void playerOrange(GameObject player) {}  // player clicked the orange button
public virtual void playerGreen(GameObject player) {}   // player clicked the green button
public virtual void playerYellow(GameObject player) {}  // player clicked the yellow button
```

By default, player input handlers are empty, which means that, in order to make something with the actions, these methods should be overwritten in the game logic script. The argument called `player` is the GameObject of the player that clicked the action button.

## Game Status

*IMPORTANT*: If you want to test your game without needing to open it from the Menu scene, you must add the `GameStatus` prefab to you game scene, and add the following lines to the `Start` function of your [Game Logic](#game-logic) script:

```cs
void Start()
{
    if (!GameStatus.instance.playing) {
        GameStatus.instance.startMiniGame(4, SceneManager.GetActiveScene().name, false);
    }

    ...
}
```

This code will start a game with only this mini game with 4 players (the number can be changed for testing purposes). Otherwise, if you don't need to test your scene individually, you don't need to have this GameObject on your scene, since he will be added in the Menu scene and will not be destroyed during all the execution.

---

The `GameStatus` is the class responsible for handling the game. It has the following properties and methods:

```cs
public class GameStatus : MonoBehaviour
{
    // ------ PROPERTIES ------

    public bool playing { get; private set; }       // Boolean indicating if it is playing
    public int miniGameCount { get; private set; }  // Number of available mini games
    public int playerCount { get; private set; }    // Number of players in the current game

    private List<string> miniGames;     // List with all mini game names
    private Queue<string> gamesToPlay;  // Queue with the mini games to play in the current game
    private List<int> playerScores;     // Current game player scores

    public static GameStatus instance;  // Game Status instance

    // ------ METHODS ------

    // Randomizes nMiniGames mini games and adds them to the queue to start a game
    private void prepareGames(int nMiniGames);

    // Loads the next mini game scene or goes to the menu if the game ended
    private void loadNextMiniGame();

    // Set the playerCount variable to nPlayers and initializes the scores to 0
    private void initializePlayers(int nPlayers);

    // Function to start a game with nPlayers players and only one mini game (miniGame) on the queue
    public void startMiniGame(int nPlayers, string miniGame);

    // Starts a game with nPlayers players and nMiniGames mini games
    public void startFullGame(int nPlayers, int nMiniGames);

    // From a list of scores, return a list with the points to give to each player
    private List<int> pointsFromScore(List<int> playerScores);

    // Function called whenever a mini game is finished to give the player points and load next mini game
    public void finishMiniGame(List<int> playerPoints);
}
```

This object is built in a way that only one instance exists at a time. Whenever the game needs to access any variable of method, it should get the current instance of the GameStatus by using: `GameStatus.instance`.

## Game Initialization

The [Game Logic](#game-logic) script should also be responsible for initializing the game whenever the scene loads. This means, for example, only activating the players that are actually playing (instead of always having 4 players, if only 3 are playing, there should only be 3 players playing the game). An example initialization code could be found either in the [CaptainLogic.cs](./Scripts/Captain/CaptainLogic.cs) file or the [RythmLogic.cs](./Scripts/Rythm/RythmLogic.cs) file.

## Game Ending

When the game finishes, it should call the function `finishMiniGame` from the [Game Status](#game-status), sending as arguemnt the game scores. These scores will be sent to the `pointsFromScore` function, which will take care of treating them and returning only the game points. For example:

```
If the game scores is: [200, 400, 150, 10]
It means that the ranking of the players is: 2 -> 1 -> 3 -> 4
So it will return [2, 3, 1, 0]
Which means that player 2 will get 3 points, and so on and so forth.
```



