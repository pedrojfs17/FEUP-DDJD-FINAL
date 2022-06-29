using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Diagnostics;
using System.Threading;

public class FallLogic : GameLogic
{
    private bool playing = false;
    private int[] scores = new int[4];
    public Stopwatch watch = new Stopwatch();

    // Start is called before the first frame update
    void Start()
    {
        playing = true;

        if (!GameStatus.instance.playing) {
            GameStatus.instance.startMiniGame(4, SceneManager.GetActiveScene().name, false);
        }

        watch = Stopwatch.StartNew();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playing) return;
        if(watch.ElapsedMilliseconds > 30000){
            watch.Stop(); 
            End();
        }
    }

    void End(){
        List<int> finalScore = new List<int>();
        finalScore.Add(GameObject.Find("Player1").GetComponent<PlayerPlatform>().getHealth());
        UnityEngine.Debug.Log(GameObject.Find("Player1").GetComponent<PlayerPlatform>().getHealth());
        finalScore.Add(GameObject.Find("Player2").GetComponent<PlayerPlatform>().getHealth());
        UnityEngine.Debug.Log(GameObject.Find("Player2").GetComponent<PlayerPlatform>().getHealth());
        finalScore.Add(GameObject.Find("Player3").GetComponent<PlayerPlatform>().getHealth());
        UnityEngine.Debug.Log(GameObject.Find("Player3").GetComponent<PlayerPlatform>().getHealth());
        finalScore.Add(GameObject.Find("Player4").GetComponent<PlayerPlatform>().getHealth());
        UnityEngine.Debug.Log(GameObject.Find("Player4").GetComponent<PlayerPlatform>().getHealth());


        //GameStatus.instance.finishMiniGame(finalScore);
    }

    public void IncreaseHealth(){
        //if(GameObject.Find("Player1").GetComponent<PlayerPlatform>().getCanGetMore() == false && GameObject.Find("Player2").GetComponent<PlayerPlatform>().getCanGetMore() == false &&GameObject.Find("Player3").GetComponent<PlayerPlatform>().getCanGetMore() == false && GameObject.Find("Player4").GetComponent<PlayerPlatform>().getCanGetMore() == false){
        //    End();
        //}
        //else{
        GameObject.Find("Player1").GetComponent<PlayerPlatform>().MoreHealth();
        GameObject.Find("Player2").GetComponent<PlayerPlatform>().MoreHealth();
        GameObject.Find("Player3").GetComponent<PlayerPlatform>().MoreHealth();
        GameObject.Find("Player4").GetComponent<PlayerPlatform>().MoreHealth();
        //}
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
