using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUIManager : TextUpdate
{
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        // Change text
        GetComponent<TextMeshProUGUI>().text = this.beforeText + score;
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetScore(int score)
    {
        this.score = score;
        this.SetText(score.ToString());
    }
}
