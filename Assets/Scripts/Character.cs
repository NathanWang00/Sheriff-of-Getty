using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Damageable
{
    // Inherits the damageable code and adds knockback, movement, and turnsystem
    // Seperating into Damageable -> Character -> PlayerCharacter / Enemy because of turn and possible AI reasons

    // Someone tell me (Nathan) if they know a better way to do this while including inheritance. Also I know it's a little overkill but I already had the code.
    public enum States
    {
        Still,
        Running,
        Midair,
        //Hitstun, only needed if they take damage on their turn which shouldn't really happen
        Dead,

        // Player specific stuff
        Menu,
    }

    [SerializeField] // Serialied for debugging, changing it won't do anything
    public States state;

    protected bool dead = false, turnActive = false;

    protected virtual void Start()
    {
        state = States.Still;
    }

    protected override void FixedUpdate()
    {
        // For handling things that occur to a character over a period of time like friction or leftover momentumn
        // One time transitions happen in the fuctions like setting a bool or jumping
        switch (state) 
        {
            case States.Still:
                break;

            case States.Running:
                break;

            case States.Midair:
                break;

            case States.Dead:
                break;
        }
        base.FixedUpdate();
    }

    protected override void Die()
    {
        dead = true;
        base.Die();
    }
}
