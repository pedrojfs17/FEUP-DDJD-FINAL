using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class CutsceneManager : MonoBehaviour
{ 
    List<int> playerPositions;
    List<int> playerScores;
    List<int> POSITION_POINTS = new List<int> { 5, 3, 1, 0 };

    Queue<Tuple<PlayableDirector, TextMeshProUGUI, ParticleSystem, int>> players;

    int CountFPS = 30;
    float NumberAnimationDuration = 3f;

    void Start()
    {
        playerPositions = GameStatus.instance.lastGamePositions;
        playerScores = GameStatus.instance.playerScores;

        players = new Queue<Tuple<PlayableDirector, TextMeshProUGUI, ParticleSystem, int>>();

        Transform scores = GameObject.Find("Scores").transform;
        Transform effects = GameObject.Find("Effects").transform;

        Transform blue = GameObject.Find("BlueAnimations").transform;
        Transform orange = GameObject.Find("OrangeAnimations").transform;
        Transform green = GameObject.Find("GreenAnimations").transform;
        Transform yellow = GameObject.Find("YellowAnimations").transform;
        
        players.Enqueue(new Tuple<PlayableDirector, TextMeshProUGUI, ParticleSystem, int>(
            blue.GetChild(playerPositions[0] - 1).GetComponent<PlayableDirector>(),
            scores.GetChild(0).GetComponent<TextMeshProUGUI>(),
            effects.GetChild(0).GetComponent<ParticleSystem>(),
            0
        ));

        players.Enqueue(new Tuple<PlayableDirector, TextMeshProUGUI, ParticleSystem, int>(
            orange.GetChild(playerPositions[1] - 1).GetComponent<PlayableDirector>(),
            scores.GetChild(1).GetComponent<TextMeshProUGUI>(),
            effects.GetChild(1).GetComponent<ParticleSystem>(),
            1
        ));
        
        if (playerPositions.Count > 2) {
            players.Enqueue(new Tuple<PlayableDirector, TextMeshProUGUI, ParticleSystem, int>(
                green.GetChild(playerPositions[2] - 1).GetComponent<PlayableDirector>(),
                scores.GetChild(2).GetComponent<TextMeshProUGUI>(),
                effects.GetChild(2).GetComponent<ParticleSystem>(),
                2
            ));
        }
        
        if (playerPositions.Count > 3) {
            players.Enqueue(new Tuple<PlayableDirector, TextMeshProUGUI, ParticleSystem, int>(
                yellow.GetChild(playerPositions[3] - 1).GetComponent<PlayableDirector>(),
                scores.GetChild(3).GetComponent<TextMeshProUGUI>(),
                effects.GetChild(3).GetComponent<ParticleSystem>(),
                3
            ));
        }

        StartCoroutine(waitForAnimation(1.5f));
    }

    private void StartNextAnimation()
    {
        if (players.Count == 0) {
            StartCoroutine(finishCutscene());
            return;
        }

        Tuple<PlayableDirector, TextMeshProUGUI, ParticleSystem, int> player = players.Dequeue(); 

        // Start Animation
        player.Item1.Play();

        // Update Text
        UpdateText(player.Item2, player.Item3, player.Item4);

        StartCoroutine(waitForAnimation(5.0f));
    }

    IEnumerator finishCutscene()
    {
        yield return new WaitForSeconds(2.0f);

        GameStatus.instance.finishCutscene();
    }

    IEnumerator waitForAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        StartNextAnimation();
    }

    private void UpdateText(TextMeshProUGUI scoreText, ParticleSystem effect, int playerIndex)
    {
        // Get round points and total score
        int roundPoints = POSITION_POINTS[playerPositions[playerIndex] - 1];
        int totalScore = playerScores[playerIndex] + roundPoints;

        scoreText.text = "+0";

        StartCoroutine(CountText(scoreText, roundPoints, true));
        StartCoroutine(TotalPoints(scoreText, effect, totalScore));
    }

    private IEnumerator CountText(TextMeshProUGUI scoreText, int newValue, bool withPlusSign)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS);

        string text = withPlusSign ? scoreText.text.Remove(0, 1) : scoreText.text;

        int previousValue;
        int.TryParse(text, out previousValue);

        int step = Mathf.CeilToInt(newValue / (CountFPS * NumberAnimationDuration)); 

        while(previousValue < newValue) {
            previousValue = Mathf.Min(previousValue + step, newValue);

            scoreText.text = withPlusSign ? "+" : "";
            scoreText.text += previousValue.ToString("N0");

            yield return Wait;
        }
    }

    IEnumerator TotalPoints(TextMeshProUGUI scoreText, ParticleSystem effect, int points)
    {
        yield return new WaitForSeconds(2.0f);

        effect.Play();

        scoreText.text = "0";
        StartCoroutine(CountText(scoreText, points, false));
    }
}
