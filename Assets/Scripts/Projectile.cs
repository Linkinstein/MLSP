using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private bool fired = false;
    private Vector2 direction;
    public bool fromPlayer = false;
    public int damage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ShootAt(Vector2 TargetVector)
    {
        direction = (TargetVector - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        fired = true;
    }

    private void Update()
    {
        if (fired)
        {
            Vector2 force = direction * speed;
            rb.velocity = new Vector2(force.x, force.y);
        }
        if (!IsVisibleFromCamera())
        {
            Destroy(this.gameObject);
        }
    }

    private bool IsVisibleFromCamera()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!fromPlayer && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
