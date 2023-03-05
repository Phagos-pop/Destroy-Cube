using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube prefab;
    [SerializeField] private float minDelay;
    [SerializeField] private float maxDelay;
    [SerializeField] private int cubeCount;
    
    private List<Cube> cubes;

    public event Action DeadCubeEvent;


    public void Start()
    {
        SpawnStart();
    }

    private void SpawnStart()
    {
        cubes = new List<Cube>(cubeCount);
        for (int i = 0; i < cubes.Capacity; i++)
        {
            SpawnCube();
        }
    }

    private void SpawnCube()
    {
        var cube = Instantiate(prefab, GetRandomPosition(), Quaternion.identity);
        cubes.Add(cube);
        cube.cubeInteractor.DeadCubeEvent += Cube_DeadCubeEvent;
    }

    private void Cube_DeadCubeEvent(Cube cube)
    {
        DeadCubeEvent?.Invoke();
        cube.cubeInteractor.DeadCubeEvent -= Cube_DeadCubeEvent;
        cubes.Remove(cube);
        Destroy(cube.gameObject);
        StartCoroutine(SpawnCouratine());
    }

    private IEnumerator SpawnCouratine()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minDelay, maxDelay));
        SpawnCube();
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 vector = new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), transform.position.y + 0.5f, UnityEngine.Random.Range(-4.5f, 4.5f));
        return vector;
    }
}
