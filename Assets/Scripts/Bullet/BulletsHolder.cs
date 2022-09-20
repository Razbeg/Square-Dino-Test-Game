using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletsHolder : MonoBehaviour
{
    [Header("Required Settings")]
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private Transform shootFrom = null;


    [Header("Game Settings")]
    [SerializeField] private int initialBulletsCount = 10;


    public Vector3 ShootFromPosition => shootFrom.position;


    private List<GameObject> bullets = new List<GameObject>();


    private void Start()
    {
        for (var i = 0; i < initialBulletsCount; i++)
            CreateBulletAndAddToList();
    }


    public Bullet GetBullet()
    {
        foreach (var bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
                return bullet.GetComponent<Bullet>();
        }

        CreateBulletAndAddToList();

        return bullets.Last().GetComponent<Bullet>();
    }

    private void CreateBulletAndAddToList()
    {
        var bullet = Instantiate(bulletPrefab, transform);
        bullet.SetActive(false);

        bullets.Add(bullet);
    }
}
