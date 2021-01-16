using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class MathC
{
    public static Vector3 FlipYZ(Vector3 toFlip)
    {
        return new Vector3(toFlip.x, toFlip.z, toFlip.y);
    }
}

