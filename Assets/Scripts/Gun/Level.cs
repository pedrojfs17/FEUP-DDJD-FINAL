// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// enum GunMiniGameState {
//     Preparing,
//     WaitingForCountdown,
//     WaitingforPlayerToFire,

//     WaitingForNextRound,
//     Finished
// }

// public class Level : MonoBehaviour
// {
//     private int playerLeft = 1;
//     private int playerRight = 2;
//     private int playerBenchLeft = 3;
//     private int playerBenchRight = 4;
//     private int[] winners = new int[4];
//     private int[] losers = new int[4];
//     private Vector3 playerLeftPosition;
//     private Vector3 playerRightPosition;
//     private Vector3 playerBenchLeftPosition;
//     private Vector3 playerBenchRightPosition;
//     private GameObject[] players;
//     private GameObject[] scoresGUI;
//     private int[] scores = new int[4];
//     private GameObject roundGUI;
//     private GameObject waitGoGUI;
    
    
//     private GunMiniGameState currentState = GunMiniGameState.Preparing;
//     private int currentRound = 1;

//     // Start is called before the first frame update
//     void Start()
//     {
//         // Get game object with name Round
//         roundGUI = GameObject.Find("Round");
//         waitGoGUI = GameObject.Find("Wait Go");
//         players = GameObject.FindGameObjectsWithTag("Player");
//         scoresGUI = GameObject.FindGameObjectsWithTag("Score");
//         playerLeftPosition = players[0].transform.position;
//         playerRightPosition = players[1].transform.position;
//         playerBenchLeftPosition = players[2].transform.position;
//         playerBenchRightPosition = players[3].transform.position;

//         updateRound(1);

//     }

//     // Update is called once per frame
//     void Update()
//     {

//         if(currentState == GunMiniGameState.WaitingForNextRound)
//         {
//             if(Input.GetKeyDown(KeyCode.N))
//             {

//                 updateRound(currentRound);
//                 return;
//             }
//         }

//         if(currentState == GunMiniGameState.WaitingforPlayerToFire) 
//         {
//             if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.L))
//             {
//                 int winner = 0;

//                 if(Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.L))
//                 {

//                     SetGUIText(waitGoGUI, "It was a tie!\nPress N to repeat");
//                     currentState = GunMiniGameState.WaitingForNextRound;
//                     return;

//                 }
//                 else if(Input.GetKeyDown(KeyCode.A))
//                 {
//                     winner = playerLeft;

//                 }
//                 else if(Input.GetKeyDown(KeyCode.L))
//                 {   
//                     winner = playerRight;
//                 }

//                 SetGUIText(waitGoGUI, "Player " + winner + " Won\nPress N to continue");
//                 SetScore(winner);
//                 currentRound++;
//                 currentState = GunMiniGameState.WaitingForNextRound;

//             }
//         }

//     }


//     private void updateRound(int round)
//     {
//         if(round > 4)
//         {
//             FinishGame();
//             return;
//         }


//         currentState = GunMiniGameState.Preparing;
        


//         Debug.Log("Round " + round);

//         PrepareRound(round);
        
//         StartCoroutine(DoRound());
//     }

//     private void PrepareRound(int round){
//         switch (round)
//         {
//             case 1:
//                 break;
//             case 2:
//                 playerLeft = 3;
//                 playerRight = 4;
//                 playerBenchLeft = 1;
//                 playerBenchRight = 2;
//                 break;
//             case 3:
//                 playerLeft = losers[0];
//                 playerRight = losers[1];
//                 playerBenchLeft = winners[0];
//                 playerBenchRight = winners[1];
//                 break;
//             case 4:
//                 playerLeft = winners[0];
//                 playerRight = winners[1];
//                 playerBenchLeft = losers[0];
//                 playerBenchRight = losers[1];
//                 break;

//             default:
//                 break;
//         }
        
//         UpdatePlayers();

//         UpdateGUI();
//     }

//     private void UpdatePlayers()
//     {

//         UpdatePlayersPosition();

//     }

//     private void UpdatePlayersPosition()
//     {
//         players[playerLeft - 1].GetComponent<Player>().GoToPosition(playerLeftPosition, Quaternion.Euler(0, 0, 0));
//         players[playerRight - 1].GetComponent<Player>().GoToPosition(playerRightPosition, Quaternion.Euler(0, 180, 0));
//         players[playerBenchLeft - 1].GetComponent<Player>().GoToPosition(playerBenchLeftPosition, Quaternion.Euler(0, 90, 0));
//         players[playerBenchRight - 1].GetComponent<Player>().GoToPosition(playerBenchRightPosition, Quaternion.Euler(0, 90, 0));

//     }

//     private void UpdateGUI()
//     {
//         // Call method SetText from roundGUI
//         SetGUIText(roundGUI, "Round " + currentRound);
//         SetGUIText(waitGoGUI, "Wait for go...");
//     }

//     public void SetScore(int playerNumber)
//     {        
//         winners[currentRound - 1] = playerNumber;
//         losers[currentRound - 1] = (playerNumber == playerLeft) ? playerRight : playerLeft;
        
//         if (currentRound == 1 || currentRound == 2)
//         {
//             scores[playerNumber - 1] += 2;

//         }else if (currentRound == 3)
//         {
//             scores[playerNumber - 1] += 1;

//         }else if (currentRound == 4)
//         {
//             scores[playerNumber - 1] += 3;
//         }


//         UpdateGUIScore(playerNumber);
//     }
    
//     private void UpdateGUIScore(int playerNumber)
//     {
//         scoresGUI[playerNumber - 1].GetComponent<ScoreUIManager>().SetScore(scores[playerNumber - 1]);

//     }

//     private IEnumerator DoRound()
//     {
//         // Generate random float number between 1 and 6
//         float countdown = Random.Range(1f, 6f);
//         currentState = GunMiniGameState.WaitingForCountdown;
//         yield return new WaitForSeconds(countdown);
        
//         currentState = GunMiniGameState.WaitingforPlayerToFire;
//         SetGUIText(waitGoGUI, "GO!!!");

//     }

//     private void FinishGame()
//     {
//         currentState = GunMiniGameState.Finished;
//         SetGUIText(waitGoGUI, "Player " + winners[3] + " won!");        
//     }

//     private void SetGUIText(GameObject guiObj, string text)
//     {
//         guiObj.GetComponent<TextUpdate>().SetText(text);
//     }
// }
