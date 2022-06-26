using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class FinalCutsceneManager : MonoBehaviour
{
    private int numPlayers;
    private List<int> playerScores;
    private List<int> playerPositions;

    [SerializeField] private List<GameObject> birds;
    [SerializeField] private List<BoxCollider> areas;
    [SerializeField] private Transform center;
    [SerializeField] private TextMeshProUGUI winnerText;

    private List<List<GameObject>> spawnedObjects;
    private List<TextMeshProUGUI> scoreTexts;

    void Start()
    {
        Transform scores = GameObject.Find("Scores").transform;
        spawnedObjects = new List<List<GameObject>>();
        scoreTexts = new List<TextMeshProUGUI>();

        numPlayers = GameStatus.instance.playerCount;
        playerScores = GameStatus.instance.playerScores;
        playerPositions = GameStatus.instance.playerPositions;

        for (int i = 0; i < numPlayers; i++) {
            scoreTexts.Add(scores.GetChild(i).GetComponent<TextMeshProUGUI>());
            spawnedObjects.Add(new List<GameObject>());
            for (int j = 0; j < playerScores[i]; j++) {
                SpawnInArea(birds[i], areas[i], i);
            }
        }

        StartCoroutine(StartCutscene());
    }

    IEnumerator StartCutscene()
    {
        yield return new WaitForSeconds(1.5f);

        GetComponent<PlayableDirector>().Play();

        StartCoroutine(DestroyExtraPigeons());
        StartCoroutine(UpdatePlayerScores());
    }

    void SpawnInArea(GameObject spawnable, BoxCollider area, int index)
    {
        Vector3 position = area.transform.position + new Vector3(
            Random.Range(-area.size.x / 2, area.size.x / 2),
            -0.5f,
            Random.Range(-area.size.z / 2, area.size.z / 2)
        );

        GameObject newBird = Instantiate(spawnable, position, Quaternion.identity);
        newBird.transform.parent = area.transform;
        newBird.transform.LookAt(center, Vector3.up);

        spawnedObjects[index].Add(newBird);
    }

    IEnumerator DestroyExtraPigeons()
    {
        yield return new WaitForSeconds(3.5f);

        int highScore = playerScores.Max();
        int secondScore = playerScores.Where(x => x < highScore).Max();

        for (int i = 0; i < numPlayers; i++) {
            int toRemove = spawnedObjects[i].Count < secondScore ? spawnedObjects[i].Count : secondScore;
            for (int j = 0; j < toRemove; j++) {
                Destroy(spawnedObjects[i][j]);
            }
        }
    }

    IEnumerator UpdatePlayerScores()
    {
        yield return new WaitForSeconds(11.0f);

        for (int i = 0; i < numPlayers; i++) {
            StartCoroutine(CountText(scoreTexts[i], playerScores[i]));
        }

        StartCoroutine(SetWinner());
    }

    private IEnumerator CountText(TextMeshProUGUI scoreText, int newValue)
    {
        if (newValue == 0) {
            scoreText.text = "0";
            yield return 0;
        }
        
        WaitForSeconds Wait = new WaitForSeconds(1f / 30f);

        int previousValue;
        int.TryParse(scoreText.text, out previousValue);

        int step = Mathf.CeilToInt(newValue / 90f); 

        while(previousValue < newValue) {
            previousValue = Mathf.Min(previousValue + step, newValue);
            scoreText.text = previousValue.ToString("N0");

            yield return Wait;
        }
    }

    IEnumerator SetWinner()
    {
        yield return new WaitForSeconds(1.0f);

        List<int> winners = Enumerable.Range(0, playerPositions.Count).Where(i => playerPositions[i] == 1).ToList();

        if (winners.Count > 1)
            winnerText.text = "DRAW";
        else
            winnerText.text = "WINNER\n" + GameStatus.ColorMapping[winners[0]];

        StartCoroutine(EndCutscene());
    }

    IEnumerator EndCutscene()
    {
        yield return new WaitForSeconds(8.0f);

        GameStatus.instance.finishGame();
    }
}
