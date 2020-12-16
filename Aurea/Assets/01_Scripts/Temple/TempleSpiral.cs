using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class TempleSpiral : MonoBehaviour
{
    // number of coils in the spiral (i.e. complete rotations)
    [SerializeField]
    private float coils = 10;
    // distance between points to plot
    [SerializeField]
    private float chord = 20;
    // outer radius of the spiral
    [SerializeField]
    private float radius = 450;
    // centre
    [SerializeField]
    private float centerX = 0;
    [SerializeField]
    private float centerY = 0;
    [SerializeField]
    private float height = 0;

    // spiral is rotated by this number of radians
    private float rotation = -Mathf.PI / 2;

    // value of theta corresponding to end of last coil
    private float thetaMax;

    // How far to step away from center for each side.
    private float awayStep;

    // direction either +1 or -1
    [SerializeField]
    int direction = -1;


    private Vector3[] points;
    public Vector3[] Setpoints(int numberpoints)
    {
        points = new Vector3[numberpoints];
        thetaMax = coils * 2 * Mathf.PI;
        awayStep = radius / thetaMax;
        // For every side, step around and away from center.
        // start at the angle corresponding to a distance of chord
        // away from centre.
        var theta = chord / awayStep;
        for(var i = 0; i < numberpoints; i++)
        {
            //
            // How far away from center
            var away = awayStep * theta;
            //
            // How far around the center.
            var around = direction * theta + rotation;
            //
            // Convert 'around' and 'away' to X and Y.
            var x = centerX + Mathf.Cos(around) * away;
            var y = centerY + Mathf.Sin(around) * away;


            points[i] = new Vector3(x, height, y);
        // to a first approximation, the points are on a circle
        // so the angle between them is chord/radius
        theta += chord / away;
        }
        
        return points;
    }

}
