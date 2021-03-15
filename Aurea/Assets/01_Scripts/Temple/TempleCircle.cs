using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleCircle : MonoBehaviour
{
    [SerializeField]
    private float radius = 450;
    // centre
    [SerializeField]
    private float centerX = 0;
    [SerializeField]
    private float centerY = 0;
    [SerializeField]
    private float height = 0;

    // Start is called before the first frame update
    public Vector3[] Setpoints(int numberpoints)
    {
        numberpoints = numberpoints + 1;
        Vector3[] points = new Vector3[numberpoints + 1];
        for (int i = 0; i < numberpoints; i++)
            points[i] = new Vector3(radius * Mathf.Cos((i + 1) * Mathf.PI *2 /numberpoints), height, radius * Mathf.Sin((i + 1) * Mathf.PI * 2 / numberpoints));
        return points;
    }
}
