using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameMenu; 

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

    public void startFullGame()
    {
        GameStatus.instance.startFullGame(4, 3);
    }

    public void startMiniGame(string game)
    {
        GameStatus.instance.startMiniGame(4, game);
    }
}
