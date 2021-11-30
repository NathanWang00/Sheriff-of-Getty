using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    [SerializeField]
    protected float damage, knockback;
    public float bulletSpeed;
    public float bulletDamage;
    public float damageFallOffSpeed;

    public Rigidbody2D rb2D;

    public void Shoot(bool invert)
    {
        Vector3 direction = transform.right;
        if (invert)
            direction = -direction;
        // shoot bullet
        rb2D.velocity = (direction * bulletSpeed);
        // destroy bullet after 5 seconds
        Destroy(gameObject, 5.0f);
    }

    private void FixedUpdate()
    {
        // bullet damage drop off
        // change when we can actually playtest
        bulletDamage -= damageFallOffSpeed * Time.deltaTime;
    }

    // Uses the collision matrix to get the right hitbox
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable d = collision.transform.root.GetComponent<Damageable>();
        if (d != null)
        {
            Vector2 direction = collision.transform.position - transform.position;
            direction = direction.normalized * knockback;

            GameManager.Instance.Damage(d, damage, direction);
        }
    }
}