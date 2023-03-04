using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonsGun : MonoBehaviour
{
    [SerializeField] private BallonBullet bullet;
    [Range(0,100)]
    [SerializeField] private float speed;
    
    [SerializeField] private float reloadingDelay;
    [SerializeField] private float shotDelay;

    [SerializeField] private Transform point0;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    private BallonBullet currentBullet;
    private Plane plane;
    private bool isCanShot;
    private int bulletCount;

    public int maxBulletCount;
    public event Action<int> BulletCountEvent;


    private void Start()
    {
        isCanShot = true;
        plane = new Plane(Vector3.up, Vector3.zero);
        currentBullet = Instantiate(bullet);
        currentBullet.gameObject.transform.position = GetPoint(point0.position, point1.position, point2.position, Vector3.zero, 0);
        StartCoroutine(Reloading());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isCanShot)
        {
            StartCoroutine(ShotCouratine(Input.mousePosition, currentBullet));
        }
    }

    private IEnumerator Reloading()
    {
        yield return null;
        while (true)
        {
            if (bulletCount < maxBulletCount)
            {
                bulletCount++;
                BulletCountEvent?.Invoke(bulletCount);
            }
            yield return new WaitForSeconds(reloadingDelay);
        }
    }

    private IEnumerator ShotCouratine(Vector3 mousePos, BallonBullet bullet)
    {
        if (bulletCount == 0)
        {
            yield break;
        }

        
        StartCoroutine(ShotDelayCouratine());
        yield return null;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Vector3 point3 = new Vector3();

        if (plane.Raycast(ray, out float position))
        {
            point3 = ray.GetPoint(position);
            if (point3.x > 4.5 || point3.x < -4.5 || point3.z > 4.5 || point3.z < -4.5)
            {
                yield break;
            }
        }

        currentBullet = Instantiate(bullet);
        currentBullet.gameObject.transform.position = GetPoint(point0.position, point1.position, point2.position, Vector3.zero, 0);
        bulletCount--;
        BulletCountEvent?.Invoke(bulletCount);

        Vector3 offset = new Vector3(point3.x, 0, point3.z);
        float ti = 0;
        while (ti < 1)
        {
            ti += 0.02f;
            bullet.gameObject.transform.position = GetPoint(point0.position, point1.position + offset, point2.position + offset, point3, ti);
            yield return new WaitForSeconds(1 / speed);
        }

        bullet.Explosione();

    }

    private IEnumerator ShotDelayCouratine()
    {
        yield return null;
        isCanShot = false;
        yield return new WaitForSeconds(shotDelay);
        isCanShot = true;
    }

    private Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2,Vector3 p3, float t)
    {
        Vector3 p01 = Vector3.Lerp(p0, p1, t);
        Vector3 p12 = Vector3.Lerp(p1, p2, t);
        Vector3 p23 = Vector3.Lerp(p2, p3, t);

        Vector3 p012 = Vector3.Lerp(p01, p12, t);
        Vector3 p123 = Vector3.Lerp(p12, p23, t);

        Vector3 p0123 = Vector3.Lerp(p012, p123, t);

        return p0123;
    }
}
