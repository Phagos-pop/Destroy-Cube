using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lerpTime;
    [SerializeField] private float lerpStep;
    [SerializeField] private float minDelayBeforeDash;
    [SerializeField] private float maxDelayBeforeDash;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeedIncrease;
    [SerializeField] private float dis;

    public event Action<Cube> DeadCubeEvent;

    private float currentSpeed;
    private Vector3 position;
    private Vector3 direction;
    private bool isMove;


    private void Start()
    {
        isMove = true;
        currentSpeed = speed;
        direction = transform.forward;
        StartCoroutine(CubeCouratine());
        StartCoroutine(CubeSpeedCouratine());
    }
    
    private void Update()
    {
        if (isMove)
        {
            transform.Translate(direction * Time.deltaTime * currentSpeed);
        }
        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
    }

    public void Death()
    {
        DeadCubeEvent?.Invoke(this);
        StopAllCoroutines();
    }

    private IEnumerator CubeSpeedCouratine()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minDelayBeforeDash, maxDelayBeforeDash));
            currentSpeed *= dashSpeedIncrease;
            yield return new WaitForSeconds(dashTime);
            currentSpeed = speed;
        }
    }

    private IEnumerator CubeCouratine()
    {
        while (true)
        {
            isMove = false;
            position = GetRandomPosition();

            var rayDirect = position - transform.position;
            Debug.DrawRay(transform.position, rayDirect, Color.cyan, 10f);

            Vector3 newDir = Vector3.RotateTowards(transform.forward, rayDirect, Mathf.Rad2Deg, 0.0F);
            Quaternion rotation = Quaternion.LookRotation(newDir);


            float lerp = 0;
#if UNITY_EDITOR
            while (lerp < 0.03)
            {
                var old = transform.rotation;

                transform.rotation = Quaternion.Lerp(old, rotation, lerp);
                lerp += lerpStep * Time.deltaTime;

                yield return new WaitForSeconds(lerpTime);

            }
#else
            while (lerp < 1)
            {
                var old = transform.rotation;

                transform.rotation = Quaternion.Lerp(old, rotation, lerp);
                lerp += lerpStep;

                yield return new WaitForSeconds(lerpTime);

            }
#endif

            isMove = true;
            while (Vector3.Distance(position, transform.position) > 0.5f)
            {
                dis = Vector3.Distance(position, transform.position);
                yield return null;
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 vector = new Vector3(UnityEngine.Random.Range(-4.5f,4.5f), transform.position.y, UnityEngine.Random.Range(-4.5f, 4.5f));
        return vector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(position, Vector3.one);
    }
}
