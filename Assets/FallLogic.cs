using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallLogic : GameLogic
{
    private bool playing = false;

    // Start is called before the first frame update
    void Start()
    {
        playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playing) return;
    }

    public int GetPlayer(GameObject player){
        string name = player.name;
        char lastChar = name[name.Length - 1];
        int playerNumber = int.Parse(lastChar.ToString());

        return playerNumber;
    }

    public override void playerAction(GameObject player){
    }

    public override void playerBlue(GameObject player) {
        if(GetPlayer(player) == 1){
            GameObject.Find("Player1").GetComponent<PlayerPlatform>().GoUp();
        }
        else if(GetPlayer(player) == 2){
            GameObject.Find("Player2").GetComponent<PlayerPlatform>().GoUp();
        }
        else if(GetPlayer(player) == 3){
            GameObject.Find("Player3").GetComponent<PlayerPlatform>().GoUp();
        }
        else if(GetPlayer(player) == 4){
            GameObject.Find("Player4").GetComponent<PlayerPlatform>().GoUp();
        }
    }

    public override void playerOrange(GameObject player) {
        if(GetPlayer(player) == 1){
            GameObject.Find("Player1").GetComponent<PlayerPlatform>().GoLeft();
        }
        else if(GetPlayer(player) == 2){
            GameObject.Find("Player2").GetComponent<PlayerPlatform>().GoLeft();
        }
        else if(GetPlayer(player) == 3){
            GameObject.Find("Player3").GetComponent<PlayerPlatform>().GoLeft();
        }
        else if(GetPlayer(player) == 4){
            GameObject.Find("Player4").GetComponent<PlayerPlatform>().GoLeft();
        }
    }

    public override void playerGreen(GameObject player){
        if(GetPlayer(player) == 1){
            GameObject.Find("Player1").GetComponent<PlayerPlatform>().GoRight();
        }
        else if(GetPlayer(player) == 2){
            GameObject.Find("Player2").GetComponent<PlayerPlatform>().GoRight();
        }
        else if(GetPlayer(player) == 3){
            GameObject.Find("Player3").GetComponent<PlayerPlatform>().GoRight();
        }
        else if(GetPlayer(player) == 4){
            GameObject.Find("Player4").GetComponent<PlayerPlatform>().GoRight();
        }
    }

    public override void playerYellow(GameObject player){
        if(GetPlayer(player) == 1){
            GameObject.Find("Player1").GetComponent<PlayerPlatform>().GoDown();
        }
        else if(GetPlayer(player) == 2){
            GameObject.Find("Player2").GetComponent<PlayerPlatform>().GoDown();
        }
        else if(GetPlayer(player) == 3){
            GameObject.Find("Player3").GetComponent<PlayerPlatform>().GoDown();
        }
        else if(GetPlayer(player) == 4){
            GameObject.Find("Player4").GetComponent<PlayerPlatform>().GoDown();
        }
    }
}
