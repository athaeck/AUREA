using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleCircle : MonoBehaviour
{
    [SerializeField]
    private float radius = 450;

    [SerializeField]
    private GameObject world = null;
    // centre
    [SerializeField]
    private float centerX = 0;
    [SerializeField]
    private float centerZ = 0;
    [SerializeField]
    private float height = 0;

    public Vector3[] Setpoints(int numberpoints)
    {
        gameObject.transform.position = new Vector3(centerX + world.transform.position.x, gameObject.transform.position.y, centerZ + world.transform.position.z);
        
        numberpoints = numberpoints + 1;
        Vector3[] points = new Vector3[numberpoints + 1];
        for (int i = 0; i < numberpoints; i++)
            points[i] = new Vector3(radius * Mathf.Cos((i + 1) * Mathf.PI *2 /numberpoints), height, radius * Mathf.Sin((i + 1) * Mathf.PI * 2 / numberpoints));
        return points;
    }
}
