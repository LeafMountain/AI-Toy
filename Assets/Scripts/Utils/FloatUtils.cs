using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class FloatUtils
{
    public static bool IsZero(this float3 value)
    {
        return value.x == 0 & value.y == 0 & value.z == 0;
    }

    public static float3 Rotate(this float3 v, float degrees)
    {
        return v.RotateRadians(degrees * UnityEngine.Mathf.Deg2Rad);
    }

    public static float3 RotateRadians(this float3 v, float radians)
    {
        var ca = math.cos(radians);
        var sa = math.sin(radians);
        return new float3(ca * v.x - sa * v.z, 0, sa * v.x + ca * v.z);
    }

}
