using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowingGameLogic : MonoBehaviour
{
    GameControlls gameControlls;
    BoatMovement leftBoatMovement, rightBoatMovement;
    
    public List<List<Action>> actions = new List<List<Action>>();
    public int[] correspondence = {0, 1, 2, 3};
    
    public GameObject obstacle;
    
    // Start is called before the first frame update
    void Start()
    {
        placeObstacles();
        setupControlls();
        setupMovement();
        assignPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameControlls.Player1.Action.IsPressed()) {
            actions[correspondence[0]][0]();
        } else {
            actions[correspondence[0]][1]();
        }
        if (gameControlls.Player2.Action.IsPressed()) {
            actions[correspondence[1]][0]();
        } else {
            actions[correspondence[1]][1]();
        }
        if (gameControlls.Player3.Action.IsPressed()) {
            actions[correspondence[2]][0]();
        } else {
            actions[correspondence[2]][1]();
        }
        if (gameControlls.Player4.Action.IsPressed()) {
            actions[correspondence[3]][0]();
        } else {
            actions[correspondence[3]][1]();
        }
        
    }
    
    private void setupControlls() {
        gameControlls = new GameControlls();
        
        gameControlls.Player1.Enable();
        gameControlls.Player2.Enable();
        gameControlls.Player3.Enable();
        gameControlls.Player4.Enable();
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
}
