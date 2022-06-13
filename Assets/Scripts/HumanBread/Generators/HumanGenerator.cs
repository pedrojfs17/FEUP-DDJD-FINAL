using System.Collections.Generic;
using UnityEngine;

public class HumanGenerator : MonoBehaviour
{
    public GameObject Human;
    public static List<GameObject> Humans = new List<GameObject>();

    private void Awake()
    {
        var positions = GameSettings.getPositions();
        foreach (var pos in positions)
        {
            Humans.Add(
                Instantiate(
                    Human,
                    new Vector3(transform.position.x + pos, transform.position.y, transform.position.z),
                    transform.rotation));
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < Humans.Count; i++)
        {
            Humans[i].GetComponent<HumanAction>().Bread = BreadGenerator.Breads[i];
            Humans[i].GetComponent<HumanMovement>().humanAction = Humans[i].GetComponent<HumanAction>();
            Humans[i].GetComponent<HumanMovement>().birdMovement = BirdGenerator.Birds[i].GetComponent<BirdMovement>();
            Humans[i].GetComponent<HumanMovement>().birdAction = BirdGenerator.Birds[i].GetComponent<BirdAction>();
        }
    }
}
