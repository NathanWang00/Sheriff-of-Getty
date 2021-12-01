using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected bool turnStart = false;
    protected Transform targetPlayer = null;
    protected Gun gun = null;
    protected override void Awake()
    {
        characterType = "Enemy";
        // No weapon switching implemented
        gun = GetComponentInChildren<Gun>();
        base.Awake();
    }

    protected override void FixedUpdate()
    {
        rb2D.AddForce(new Vector2(0, Mathf.Clamp(-rb2D.velocity.y - maxFallSpeed, -fallSpeed, fallSpeed)), ForceMode2D.Impulse);
        currentHealth = GetHealth();
        Healthbar.SetHealth(currentHealth, maxHealth);
    }

    protected override void Update()
    {
        gun.WeaponActive(currentTurn);
        if (currentTurn)
        {
            if (turnStart == false)
            {
                turnStart = true;
                targetPlayer = FindClosestEnemy();
            }

            if (targetPlayer == null)
            {
                turnStart = false;
                Debug.Log("Can't find player");
            } 
            else
            {
                if (targetPlayer.position.x > transform.position.x)
                {
                    facingRight = true;
                } else
                {
                    facingRight = false;
                }
                Vector3 localScale = transform.localScale;
                if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
                {
                    localScale.x *= -1;
                }
                transform.localScale = localScale;
                if (!gun.RotateToObject(targetPlayer))
                {
                    gun.Shoot(facingRight);
                    // Copy Character timer function when they're done
                    GameManager.Instance.NextTurn();
                }
            }
        }
        
    }

    protected virtual Transform FindClosestEnemy()
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;
        var characters = FindObjectsOfType<Character>();
        foreach (var character in characters)
        {
            if (character != dead && character.characterType == "Player")
            {
                float distance = Vector3.Distance(character.transform.position, transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = character.transform;
                }
            }
        }
        if (closest == null)
        {
            Debug.Log("No players found");
        }
        return closest;
    }
}
