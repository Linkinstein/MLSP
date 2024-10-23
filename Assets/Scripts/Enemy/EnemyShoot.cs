using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform BulletPort;
    [SerializeField] private Projectile bulletPrefab;
    [SerializeField] private EnemyAI eAI;
    [SerializeField] private EnemyAnimator eAnim;
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
        Turn();
        yield return new WaitForSeconds(ShootDelay);
        if (eAI.isChasing && cc.enabled)
        {
            eAnim.Shoot();
            Turn();
            Projectile bullet = Instantiate(bulletPrefab, BulletPort.transform.position, Quaternion.identity);
            bullet.ShootAt(eAI.targetVector);
            aS.Play();
        }
        fired = false;
    }

    private void Turn()
    {
        if (eAI.facing > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (eAI.facing < 0)
        {
            transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
