using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Architecture;

public class CubeRepository : IRepository
{
    public float speed = 1;
    public float lerpTime = 0.01f;
    public float lerpStep = 0.01f;
    public float minDelayBeforeDash = 3;
    public float maxDelayBeforeDash = 6;
    public float dashTime = 1;
    public float dashSpeedIncrease = 3;

    public void Initialize()
    {
        
    }

    public void Save()
    {
        
    }
}
