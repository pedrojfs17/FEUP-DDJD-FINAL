using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextUpdate : MonoBehaviour
{
    [SerializeField] protected string beforeText = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        // Change text
        GetComponent<TextMeshProUGUI>().text = beforeText + text;
    }
}
