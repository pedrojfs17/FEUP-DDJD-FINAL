using System.Collections.Generic;
using UnityEngine;

public class BreadGenerator : MonoBehaviour
{
    public GameObject Bread;
    public static List<GameObject> Breads = new List<GameObject>();

    private void Awake()
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
