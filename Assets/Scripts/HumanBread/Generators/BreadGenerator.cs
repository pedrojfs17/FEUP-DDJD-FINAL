using System.Collections.Generic;
using UnityEngine;

public class BreadGenerator : MonoBehaviour
{
    public GameObject Bread;
    private List<GameObject> Breads = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        var positions = GameSettings.getPositions();
        foreach (var pos in positions)
        {
            Breads.Add(
                Instantiate(
                    Bread,
                    new Vector3(transform.position.x + pos, transform.position.y, transform.position.z),
                    Bread.transform.rotation));
        }
    }
}
