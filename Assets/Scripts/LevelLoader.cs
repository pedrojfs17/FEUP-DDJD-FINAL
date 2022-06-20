using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : GameLogic
{
    bool[] playersReady = new bool[4];

    public Animator transition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate() 
    {
        if (playersReady[0] && playersReady[1] && playersReady[2] && playersReady[3]) {
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);
        
        SceneManager.LoadScene("GunLevel");

    }

    public override void playerAction(GameObject player){
        // Get GameObject name
        string name = player.name;

        // Get last character of name
        char lastChar = name[name.Length - 1];

        // Convert last character to int
        int playerNumber = int.Parse(lastChar.ToString());

        playersReady[playerNumber - 1] = true;
        
        Debug.Log("Player " + playerNumber + " is ready");
    }
}
