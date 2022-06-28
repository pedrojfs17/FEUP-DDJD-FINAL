using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private int _numPlayers = 4;
    private int _numMiniGames = 3;

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
