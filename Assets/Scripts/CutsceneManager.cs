using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class CutsceneManager : MonoBehaviour
{ 
    List<int> playerPositions;
    Queue<PlayableDirector> animations;
    Queue<Tuple<TextMeshProUGUI, int>> scoreTexts;

    int CountFPS = 30;
    float NumberAnimationDuration = 3f;

    void Start()
    {
        List<int> POSITION_POINTS = new List<int> { 5, 3, 1, 0 };

        playerPositions = GameStatus.instance.playerPositions;

        animations = new Queue<PlayableDirector>();
        scoreTexts = new Queue<Tuple<TextMeshProUGUI, int>>();

        Transform scores = GameObject.Find("Scores").transform;

        Transform blue = GameObject.Find("BlueAnimations").transform;
        Transform orange = GameObject.Find("OrangeAnimations").transform;
        Transform green = GameObject.Find("GreenAnimations").transform;
        Transform yellow = GameObject.Find("YellowAnimations").transform;
        
        PlayableDirector blueAnimation = blue.GetChild(playerPositions[0] - 1).GetComponent<PlayableDirector>();
        animations.Enqueue(blueAnimation);
        scoreTexts.Enqueue(new Tuple<TextMeshProUGUI, int>(scores.GetChild(0).GetComponent<TextMeshProUGUI>(), POSITION_POINTS[playerPositions[0] - 1]));

        PlayableDirector orangeAnimation = orange.GetChild(playerPositions[1] - 1).GetComponent<PlayableDirector>();
        animations.Enqueue(orangeAnimation);
        scoreTexts.Enqueue(new Tuple<TextMeshProUGUI, int>(scores.GetChild(1).GetComponent<TextMeshProUGUI>(), POSITION_POINTS[playerPositions[1] - 1]));
        
        if (playerPositions.Count > 2) {
            PlayableDirector greenAnimation = green.GetChild(playerPositions[2] - 1).GetComponent<PlayableDirector>();
            animations.Enqueue(greenAnimation);
            scoreTexts.Enqueue(new Tuple<TextMeshProUGUI, int>(scores.GetChild(2).GetComponent<TextMeshProUGUI>(), POSITION_POINTS[playerPositions[2] - 1]));
        }
        
        if (playerPositions.Count > 3) {
            PlayableDirector yellowAnimation = yellow.GetChild(playerPositions[3] - 1).GetComponent<PlayableDirector>();
            animations.Enqueue(yellowAnimation);
            scoreTexts.Enqueue(new Tuple<TextMeshProUGUI, int>(scores.GetChild(3).GetComponent<TextMeshProUGUI>(), POSITION_POINTS[playerPositions[3] - 1]));
        }

        StartCoroutine(waitForAnimation(1.5f));
    }

    private void StartNextAnimation()
    {
        if (animations.Count == 0) {
            GameStatus.instance.finishCutscene();
            return;
        }

        animations.Dequeue().Play();

        Tuple<TextMeshProUGUI, int> playerInfo = scoreTexts.Dequeue(); 
        UpdateText(playerInfo.Item1, playerInfo.Item2);

        StartCoroutine(waitForAnimation(5.0f));
    }

    IEnumerator waitForAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        StartNextAnimation();
    }

    private void UpdateText(TextMeshProUGUI scoreText, int newValue)
    {
        scoreText.text = "+0";
        StartCoroutine(CountText(scoreText, newValue));
    }

    private IEnumerator CountText(TextMeshProUGUI scoreText, int newValue)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS);

        int previousValue;
        int.TryParse(scoreText.text.Remove(0, 1), out previousValue);

        int step = Mathf.CeilToInt(newValue / (CountFPS * NumberAnimationDuration)); 

        while(previousValue < newValue) {
            previousValue = Mathf.Min(previousValue + step, newValue);

            scoreText.text = "+" + previousValue.ToString("N0");

            yield return Wait;
        }
    }
}
