using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdGenerator : MonoBehaviour
{
    public GameObject Bird;
    public static List<GameObject> Birds = new List<GameObject>();


    private void Awake()
    {
        var positions = GameSettings.getPositions();
        foreach (var pos in positions)
        {
            Birds.Add(
                Instantiate(
                    Bird,
                    new Vector3(transform.position.x + pos, transform.position.y, transform.position.z),
                    transform.rotation));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].GetComponent<BirdMovement>().PlayerControl.AddBinding("<Keyboard>/" + (i + 1));
            Birds[i].GetComponent<BirdAction>().Bread = BreadGenerator.Breads[i];
            Birds[i].GetComponent<BirdAction>().humanMovement = HumanGenerator.Humans[i].GetComponent<HumanMovement>();
        }
    }
}
