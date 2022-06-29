using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RowingLogic : GameLogic
{
    GameControlls gameControlls;
    
    public List<List<Action>> actions = new List<List<Action>>();
    public int[] correspondence = {0, 1, 2, 3};
    
    public GameObject obstacle, bluePigeon, orangePigeon, greenPigeon, yellowPigeon;
    private List<GameObject> pigeonPrefabs = new List<GameObject>();
    Transform leftBoat, rightBoat;
    
    [SerializeField] private TextMeshProUGUI leftCountDown, rightCountDown;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!GameStatus.instance.playing) {
            GameStatus.instance.startMiniGame(4, SceneManager.GetActiveScene().name, false);
        }
        
        leftBoat = transform.Find("LeftBoat");
        rightBoat = transform.Find("RightBoat");
        
        pigeonPrefabs.Add(bluePigeon);
        pigeonPrefabs.Add(orangePigeon);
        pigeonPrefabs.Add(greenPigeon);
        pigeonPrefabs.Add(yellowPigeon);
        
        placeObstacles();
        setupMovement();
        assignPlayers();
        
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        WaitForSeconds waitHalfSecond = new WaitForSeconds(0.5f);
        WaitForSeconds waitOneSecond = new WaitForSeconds(1);

        yield return waitOneSecond;
        yield return waitOneSecond;

        for (int i = 3; i > 0; i--) {
            leftCountDown.text = i.ToString();
            rightCountDown.text = i.ToString();
            yield return waitOneSecond;
        }

        leftCountDown.text = "START";
        rightCountDown.text = "START";

        yield return waitHalfSecond;
        leftCountDown.text = "";
        rightCountDown.text = "";
        
        leftBoat.GetComponent<BoatMovement>().playing = true;
        rightBoat.GetComponent<BoatMovement>().playing = true;
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
        BoatMovement leftBoatMovement = leftBoat.GetComponent<BoatMovement>();
        BoatMovement rightBoatMovement = rightBoat.GetComponent<BoatMovement>();
        
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
        
        for (int i = 0; i < 4; i++) {
            switch (correspondence[i]) {
                case 0:
                    Instantiate(pigeonPrefabs[i], leftBoat.Find("PlayerLeft").transform.position, Quaternion.identity, leftBoat);
                    break;
                case 1:
                    Instantiate(pigeonPrefabs[i], leftBoat.Find("PlayerRight").transform.position, Quaternion.identity, leftBoat);
                    break;
                case 2:
                    Instantiate(pigeonPrefabs[i], rightBoat.Find("PlayerLeft").transform.position, Quaternion.identity, rightBoat);
                    break;
                case 3:
                    Instantiate(pigeonPrefabs[i], rightBoat.Find("PlayerRight").transform.position, Quaternion.identity, rightBoat);
                    break;
            }
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
