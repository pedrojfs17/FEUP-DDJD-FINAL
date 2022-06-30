using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private int _numPlayers = 4;
    private int _numMiniGames = 4;

    [SerializeField] List<Animator> menuPigeons;

    private void Start() 
    {
        StartFlyingAnimations();
    }

    public void StartFlyingAnimations()
    {
        foreach (Animator pigeon in menuPigeons) {
            pigeon.SetBool("Flying", true);
        }
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
