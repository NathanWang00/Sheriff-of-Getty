using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPan : MonoBehaviour
{

    private bool isSwinging;
    private float knockback;
    private float damage;

    void Start()
    {
        isSwinging = false;
        knockback = 10;
        damage = 30;
    }

    public void EnterSwing(){
        isSwinging = true;
    }

    public void ExitSwing(){
        isSwinging = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isSwinging){
            return;
        }
        Damageable d = collision.transform.root.GetComponent<Damageable>();
        if (d != null)
        {
            Vector2 direction = collision.transform.position - transform.position;
            direction = direction.normalized * knockback;

            GameManager.Instance.Damage(d, damage, direction);
        }
    }
}