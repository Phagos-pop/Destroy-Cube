using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonBullet : MonoBehaviour
{
    [SerializeField] private float radius;

    public void Explosione()
    {
        Collider[] overlappedCollider = Physics.OverlapSphere(transform.position, radius);
        foreach (var item in overlappedCollider)
        {
            var cube =  item.GetComponent<Cube>();
            if (cube != null)
            {
                cube.Death();
            }
        }

        Destroy(this.gameObject);
    }
}
