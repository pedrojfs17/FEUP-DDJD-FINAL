using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodjeLogic : GameLogic
{
    private bool playing = false;

    void Start()
    {
        playing = true;
    }

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
        if(GetPlayer(player) == 2){
            GameObject.Find("Player2").GetComponent<Gun>().Shoot();
        }
        else if(GetPlayer(player) == 3){
            GameObject.Find("Player3").GetComponent<Gun>().Shoot();
        }
        else if(GetPlayer(player) == 4){
            GameObject.Find("Player4").GetComponent<Gun>().Shoot();
        }
    }

    public override void playerBlue(GameObject player) {
        if(GetPlayer(player) == 1){
            GameObject.Find("Player1").GetComponent<PlayerController>().GoLeft();
        }
    }

    public override void playerOrange(GameObject player) {
         if(GetPlayer(player) == 1){
            GameObject.Find("Player1").GetComponent<PlayerController>().GoRight();
        }
    }

    public override void playerGreen(GameObject player) {}

    public override void playerYellow(GameObject player) {}
}
