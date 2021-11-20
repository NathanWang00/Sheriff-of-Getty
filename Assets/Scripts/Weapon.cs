using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private GameObject currentCharacter; //cowboy character that is shooting from

    // Start is called before the first frame update
    void Start()
    {
        //this switches based on turns fix later
        currentCharacter = GameObject.Find("TestCharacter");
        //this also switches based on current character
        
        this.transform.SetParent(currentCharacter.transform);
        this.transform.position = currentCharacter.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            //rotate weapon up
        }
        if (Input.GetKey("down"))
        {
            //rotate weapon down
        }
    }
}
