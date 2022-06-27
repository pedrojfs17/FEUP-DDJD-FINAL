using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] clouds;

    [SerializeField] private float spawnInterval;

    [SerializeField] private float offsetX;

    CaptainLogic gameLogic;

    // Start is called before the first frame update
    void Start()
    {
        GameObject logic = GameObject.Find("GameLogic");
        gameLogic = logic.GetComponent<CaptainLogic>();

        Invoke("AttemptSpawn", spawnInterval);
    }

    void SpawnCloud() 
    {
        float inFrontProbability = UnityEngine.Random.Range(0, 101);

        float offsetY, offsetZ;

        if (inFrontProbability > 75) {
            // Cloud in front of the screen
            float topProbability = UnityEngine.Random.Range(0, 101);

            if (topProbability > 50) offsetY = UnityEngine.Random.Range(1f, 5f);
            else offsetY = UnityEngine.Random.Range(-5f, -3f);

            offsetZ = UnityEngine.Random.Range(-5f, -1.5f);
        } else {
            offsetY = UnityEngine.Random.Range(-8f, 8f);
            offsetZ = UnityEngine.Random.Range(2f, 15f);
        }

        Vector3 frontPlayerPosition = gameLogic.GetFirstPlayerPosition();
        Vector3 initialPosition = frontPlayerPosition + new Vector3(offsetX, offsetY, offsetZ);
        float endPositionX = frontPlayerPosition.x - offsetX;

        Vector3 scale = new Vector3(
            UnityEngine.Random.Range(0.1f, 0.15f),
            UnityEngine.Random.Range(0.1f, 0.15f),
            UnityEngine.Random.Range(0.1f, 0.15f)
        );

        float speed = UnityEngine.Random.Range(5, 10);

        int randomIndex = UnityEngine.Random.Range(0, clouds.Length);
        GameObject cloud = Instantiate(clouds[randomIndex]);

        cloud.GetComponent<CloudMovement>().StartFloating(speed, endPositionX);

        cloud.transform.position = initialPosition;
        cloud.transform.localScale = scale;
    }

    void AttemptSpawn()
    {
        if (gameLogic.playing) {
            SpawnCloud();

            Invoke("AttemptSpawn", spawnInterval);
        }
    }
}
