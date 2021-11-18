using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    protected bool actionable = true;

    protected void Update()
    {
        if (turnActive && actionable && !dead) // in theory, turn shouldn't select a dead player but whatever
        {
            // Input goes here
        }
    }
}