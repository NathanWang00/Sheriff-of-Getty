using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverObj : MonoBehaviour

{
    public int health;
    private void OnCollisionEnter2D(Collision2D other) 
    {
        //Hurt();
        if (health <= 0) Destroy(this);
    }
}