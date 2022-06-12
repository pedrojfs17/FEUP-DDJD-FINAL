using System.Collections.Generic;
using UnityEngine;

public class HumanGenerator : MonoBehaviour
{
    public GameObject Human;
    private List<GameObject> Humans = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
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
}
