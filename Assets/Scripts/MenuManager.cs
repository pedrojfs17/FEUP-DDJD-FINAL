using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameMenu; 

    private int _numPlayers = 4;
    private int _numMiniGames = 1;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
    }

    public void openGameMenu()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
    }

    public void openMainMenu()
    {
        gameMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
        print("Exit Game!");
    }

    public void setNumPlayers(int numPlayers)
    {
        _numPlayers = numPlayers;
    }

    public void setNumMiniGames(int numMiniGames)
    {
        _numMiniGames = numMiniGames;
    }

    public void startFullGame()
    {
        GameStatus.instance.startFullGame(_numPlayers, _numMiniGames);
    }

    public void startMiniGame(string game)
    {
        GameStatus.instance.startMiniGame(_numPlayers, game);
    }
}
