using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class HexMetrics 
{

    /**
     * Because a hexagon consists of a circle of six equilateral triangles, 
     * the distance from the center to any corner is also 10. This defines the outer radius of our hexagon cell.
     */
    public const float outerRadius = 10f;


    /**
     * There is also an inner radius, which is the distance from the center to each of the edges. This metric is important, 
     * because the distance to the center of each neighbor is equal to twice this value.
     * The inner radius is equal to (sqrt3/2) * the outer radius = ( 5*sqrt3)
     */
    public const float innerRadius = outerRadius * 0.866025404f;

    /*
     * the positions of the six corners relative to the cell's center.
     */
    public static Vector3[] corners = {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
                new Vector3(0f, 0f, outerRadius)

    };

}
