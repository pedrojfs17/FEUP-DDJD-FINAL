using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{ 
    Queue<PlayableDirector> animations;

    void Start()
    {
        List<int> playerPositions = GameStatus.instance.playerPositions;

        animations = new Queue<PlayableDirector>();

        Transform blue = GameObject.Find("BlueAnimations").transform;
        Transform orange = GameObject.Find("OrangeAnimations").transform;
        Transform green = GameObject.Find("GreenAnimations").transform;
        Transform yellow = GameObject.Find("YellowAnimations").transform;
        
        PlayableDirector blueAnimation = blue.GetChild(playerPositions[0] - 1).GetComponent<PlayableDirector>();
        animations.Enqueue(blueAnimation);

        PlayableDirector orangeAnimation = orange.GetChild(playerPositions[1] - 1).GetComponent<PlayableDirector>();
        animations.Enqueue(orangeAnimation);
        
        PlayableDirector greenAnimation = green.GetChild(playerPositions[2] - 1).GetComponent<PlayableDirector>();
        animations.Enqueue(greenAnimation);
        
        PlayableDirector yellowAnimation = yellow.GetChild(playerPositions[3] - 1).GetComponent<PlayableDirector>();
        animations.Enqueue(yellowAnimation);

        StartCoroutine(waitForAnimation(1.5f));
    }

    private void StartNextAnimation()
    {
        if (animations.Count == 0) {
            GameStatus.instance.finishCutscene();
            return;
        }

        animations.Dequeue().Play();

        StartCoroutine(waitForAnimation(5.0f));
    }

    IEnumerator waitForAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        StartNextAnimation();
    }
}
