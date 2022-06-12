using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static int Players = 1;
    public int[] Classification;

    void Start()
    {

    }

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
}
