using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image bulletImage;
    [SerializeField] private BallonsGun ballonsGun;
    [SerializeField] private List<Image> images;

    private void Start()
    {
        ballonsGun.BulletCountEvent += BallonsGun_BulletCountEvent;
    }

    public void OnDestroy()
    {
        ballonsGun.BulletCountEvent -= BallonsGun_BulletCountEvent;
    }

    private void BallonsGun_BulletCountEvent(int count)
    {
        SetBulletCount(count);
    }

    private void SetBulletCount(int count)
    {
        count--;
        for (int i = 0; i < images.Count; i++)
        {
            if (i <= count)
            {
                images[i].gameObject.SetActive(true);
            }
            else
            {
                images[i].gameObject.SetActive(false);
            }
        }
    }
}
