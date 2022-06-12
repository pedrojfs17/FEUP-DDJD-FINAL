using System.Collections.Generic;
using UnityEngine;

public class BirdGenerator : MonoBehaviour
{
    public GameObject Bird;
    private List<GameObject> Birds = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
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
}
