using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    [SerializeField]
    protected float damage, knockback;

    protected Rigidbody2D rb2D;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
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
