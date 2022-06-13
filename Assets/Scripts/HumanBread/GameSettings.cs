using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static int Players = 4;
    public static List<GameObject> Classification = new List<GameObject>();
    
    public static float[] getPositions()
    {
        switch (Players)
        {
            case 2:
                return new float[] { -2.5f, +2.5f };
            case 3:
                return new float[] { -3.0f, 0, +3.0f };
            case 4:
                return new float[] { -3.0f, -1.0f, +1.0f, +3.0f };
            default:
                return new float[] { 0 };
        }
    }

    public static void setClassification(GameObject bird)
    {
        Classification.Add(bird);
    }
}
