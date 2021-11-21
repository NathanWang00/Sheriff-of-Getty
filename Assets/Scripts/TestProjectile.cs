using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    [SerializeField]
    protected float damage, knockback;
    public float bulletSpeed;
    public float bulletDamage;

    protected Rigidbody2D rb2D;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb2D.AddForce(rb2D.transform.right * bulletSpeed);
        Destroy(gameObject, 5.0f);
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
