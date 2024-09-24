using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform BulletPort;
    [SerializeField] private Projectile bulletPrefab;
    [SerializeField] private EnemyAI eAI;
    [SerializeField] private CircleCollider2D cc;
    [SerializeField] private AudioSource aS;
    [SerializeField] private bool fired = false;
    [SerializeField] private float ShootDelay = 3f;


    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if (!fired && eAI.isChasing && cc.enabled)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        fired = true;
        yield return new WaitForSeconds(ShootDelay);
        if (eAI.isChasing && cc.enabled)
        {
            Projectile bullet = Instantiate(bulletPrefab, BulletPort.transform.position, Quaternion.identity);
            bullet.ShootAt(eAI.targetVector);
            aS.Play();
        }
        fired = false;
    }
}
