using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Diagnostics;
using System.Threading;

public class DodjeLogic : GameLogic
{
    private bool playing = false;
    private int[] scores = new int[4];
    public Stopwatch watch = new Stopwatch();

    void Start()
    {
        playing = true;

        if (!GameStatus.instance.playing) {
            GameStatus.instance.startMiniGame(4, SceneManager.GetActiveScene().name, false);
        }

        watch = Stopwatch.StartNew();

    }

    void Update()
    {
        if (!playing) return;
        if(watch.ElapsedMilliseconds > 30000){
            watch.Stop(); 
            End();
        }
        
    }

    void End(){
        if (GameObject.Find("Player1").GetComponent<PlayerController>().getHealth() < 250){
            List<int> finalScore = new List<int>();
            finalScore.Add(100);
            finalScore.Add(0);
            finalScore.Add(0);
            finalScore.Add(0);

            UnityEngine.Debug.Log("Won");

            //GameStatus.instance.finishMiniGame(finalScore);
        }
        else{
            List<int> finalScore = new List<int>();
            finalScore.Add(0);
            finalScore.Add(100);
            finalScore.Add(100);
            finalScore.Add(100);

            UnityEngine.Debug.Log("Lost");

            //GameStatus.instance.finishMiniGame(finalScore);
        }
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
