using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Architecture;
using System;

public class CubeInteractor : IInteractor
{
    private Cube cube;
    private CubeRepository cubeRepository;

    private float currentSpeed;
    private Vector3 position;
    private Vector3 direction;
    private bool isMove;

    public event Action<Cube> DeadCubeEvent;

    public CubeInteractor(Cube cube, CubeRepository cubeRepository)
    {
        this.cube = cube;
        this.cubeRepository = cubeRepository;
    }

    public void Update()
    {
        if (isMove)
        {
            cube.transform.Translate(direction * Time.deltaTime * currentSpeed);
        }
        Debug.DrawRay(cube.transform.position, cube.transform.forward * 10, Color.red);
    }

    public void Initialize()
    {
        currentSpeed = cubeRepository.speed;
        direction = Vector3.forward;
        isMove = true;
        cube.StartCoroutine(CubeSpeedCouratine());
        cube.StartCoroutine(CubeCouratine());
    }

    private IEnumerator CubeSpeedCouratine()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(cubeRepository.minDelayBeforeDash, cubeRepository.maxDelayBeforeDash));
        currentSpeed *= cubeRepository.dashSpeedIncrease;
        yield return new WaitForSeconds(cubeRepository.dashTime);
        currentSpeed = cubeRepository.speed;
        cube.StartCoroutine(CubeSpeedCouratine());
    }

    private IEnumerator CubeCouratine()
    {
        position = GetRandomPosition();

        var direct = position - cube.transform.position;

        Vector3 newDir = Vector3.RotateTowards(cube.transform.forward, direct, Mathf.Rad2Deg, 0.0F);
        Quaternion rotation = Quaternion.LookRotation(newDir);
        Quaternion old = cube.transform.rotation;

        float lerp = 0;
        while (Vector3.Distance(position, cube.transform.position) > 0.5f)
        {
            direct = position - cube.transform.position;
            old = cube.transform.rotation;
            newDir = Vector3.RotateTowards(cube.transform.forward, direct, Mathf.Rad2Deg, 0.0F);
            rotation = Quaternion.LookRotation(newDir);

            cube.transform.rotation = Quaternion.Lerp(old, rotation, lerp);
            lerp += cubeRepository.lerpStep;

            yield return new WaitForSeconds(cubeRepository.lerpTime);
        }

        yield return null;
        cube.StartCoroutine(CubeCouratine());
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 vector = new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), cube.transform.position.y, UnityEngine.Random.Range(-4.5f, 4.5f));
        return vector;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(position, Vector3.one);
    }

    public void Death()
    {
        DeadCubeEvent?.Invoke(cube);
        cube.StopAllCoroutines();
    }
}
