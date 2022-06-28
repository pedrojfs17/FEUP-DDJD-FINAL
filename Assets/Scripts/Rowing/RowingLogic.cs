using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RowingLogic : GameLogic
{
    GameControlls gameControlls;
    BoatMovement leftBoatMovement, rightBoatMovement;
    
    public List<List<Action>> actions = new List<List<Action>>();
    public int[] correspondence = {0, 1, 2, 3};
    
    public GameObject obstacle;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!GameStatus.instance.playing) {
            GameStatus.instance.startMiniGame(4, SceneManager.GetActiveScene().name, false);
        }
        
        placeObstacles();
        setupMovement();
        assignPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void playerAction(GameObject player) {
        int index = player.GetComponent<InputController>().playerNumber - 1;
        actions[correspondence[index]][0]();
    }
    
    public override void playerActionCanceled(GameObject player) {
        int index = player.GetComponent<InputController>().playerNumber - 1;
        actions[correspondence[index]][1]();
    }
    
    private void setupMovement() {
        leftBoatMovement = transform.Find("LeftBoat").GetComponent<BoatMovement>();
        rightBoatMovement = transform.Find("RightBoat").GetComponent<BoatMovement>();
        
        //leftBoat left side movement
        this.actions.Add(new List<Action>());
        this.actions[0].Add(leftBoatMovement.pressLeft);
        this.actions[0].Add(leftBoatMovement.raiseLeft);
        
        //leftBoat right side movement
        this.actions.Add(new List<Action>());
        this.actions[1].Add(leftBoatMovement.pressRight);
        this.actions[1].Add(leftBoatMovement.raiseRight);
        
        //rightBoat left side movement
        this.actions.Add(new List<Action>());
        this.actions[2].Add(rightBoatMovement.pressLeft);
        this.actions[2].Add(rightBoatMovement.raiseLeft);
        
        //rightBoat right side movement
        this.actions.Add(new List<Action>());
        this.actions[3].Add(rightBoatMovement.pressRight);
        this.actions[3].Add(rightBoatMovement.raiseRight);
    }
    
    private void assignPlayers() {
        for (int t = 0; t < correspondence.Length; t++ )
        {
            int tmp = correspondence[t];
            int r = UnityEngine.Random.Range(t, correspondence.Length);
            correspondence[t] = correspondence[r];
            correspondence[r] = tmp;
        }
    }
    
    private void placeObstacles() {
        for (int z = 30; z <= 165; z += 15) {
            GameObject newObstacle = Instantiate(obstacle) as GameObject;
            newObstacle.transform.position = new Vector3(UnityEngine.Random.Range(-7, 7), 0, z);
        }
    }
    
    public void finish(string winner) {
        List<int> gameScores = new List<int>();
        for (int i = 0; i <= 3; i++) {
            if (correspondence[i] <= 1) {
                if (winner == "LeftBoat") {
                    gameScores.Add(100);
                } else {
                    gameScores.Add(0);
                }
            } else {
                if (winner == "RightBoat") {
                    gameScores.Add(100);
                } else {
                    gameScores.Add(0);
                }
            }
        }
        GameStatus.instance.finishMiniGame(gameScores);
    }
}
