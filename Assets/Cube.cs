using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public CubeInteractor cubeInteractor;
    private CubeRepository cubeRepository;

    private void Start()
    {
        cubeRepository = new CubeRepository();
        cubeInteractor = new CubeInteractor(this, cubeRepository);
        cubeInteractor.Initialize();
    }

    private void Update()
    {
        cubeInteractor.Update();
    }

    public void Death()
    {
        cubeInteractor.Death();
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        cubeInteractor.OnDrawGizmos();
    }
}
