using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : MonoBehaviour
{
    public float maxAmmo;
    private float currentAmmo;

    private bool isActive;
    private bool isChoosing;

    private GameObject currentCharacter; 

    void Start()
    {
        currentAmmo = maxAmmo;
        isActive = false;
    }

    void Update()
    {
        if(!isActive){
            return;
        }
        if (Input.GetKey(KeyCode.Return)){
            //do healing
            
        }
    }

    public void BeerActive(bool active){
        isActive = active;
    }
}