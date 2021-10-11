using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFloat
{
    public float NextFloat(float min, float max)
    {
        float random = Random.value;
        double val = (double)((double)random * (double)(max - min) + min);
        return (float)val;
    }
}