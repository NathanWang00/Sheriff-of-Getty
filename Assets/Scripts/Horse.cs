using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This should handle all horsin all behavior
public class Horse : MonoBehaviour
{
    private GameObject currentCharacter; //character that can ride horse
    private bool isCharging;
    private bool isShooting;
    public float currentAmmo;
    private float chargeTime;
    private float shootPower;
    // Start is called before the first frame update
    void Start()
    {
        isCharging = false;
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        // //MOUNTING
        //     if(Input.GetKeyDown("enter")){
        //     float distance = Vector3.Distance(this.transform.position, currentCharacter.transform.position);
        //         //cowboy has to be right next to horse to mount
        //         if(distance < 0.5f && !beingRidden){
        //             Vector3 seatedPosition = new Vector3(currentCharacter.transform.position.x, currentCharacter.transform.position.y + 0.5f, 0);
        //             currentCharacter.transform.position = seatedPosition;
        //             currentCharacter.transform.parent = this.transform;
        //             beingRidden = true;
        //         }
        //         //detach cowboy from horse
        //         else if(beingRidden){
        //             currentCharacter.transform.parent = null;
        //             beingRidden = false;
        //         }
        // }
        //RIDING
        //based on player movement, so implement later

        //SHOOTING 
        // pressing enter to shoot
        if (Input.GetKeyDown(KeyCode.Return) && currentAmmo != 0){
            Debug.Log("Start");
            isCharging = true;
        }
        //charging up
        else if(isCharging && chargeTime < 3f && Input.GetKey(KeyCode.Return)){
            Debug.Log("Charging");
            chargeTime += Time.deltaTime;
        }
        //actually shoot
        else if(isCharging && (chargeTime >= 3f || Input.GetKeyUp(KeyCode.Return))){
            Debug.Log("Shooting");
            currentAmmo -= 0;
            this.transform.parent = null;
            float power = chargeTime/10;
            StartCoroutine("BoomerangHorse", power);
        }
    }

    private IEnumerator BoomerangHorse(float power){
        if(this.isShooting){
            yield break;
        }
        //Lose horse
        isShooting = true;
        currentAmmo =0;
        float timer = power * 10;
        Debug.Log(timer);
        float timePassed = 0;

        Transform thisHorse = this.GetComponent<Transform>();

        //move and spin in certain direction
        while(timePassed <= timer/2){
            timePassed += Time.deltaTime;
            thisHorse.position = thisHorse.position + new Vector3(1f, 1f, 0f);
        }
        //move back towards player AND SPIN
        timePassed = 0;
        while(timePassed <= timer/2){
            timePassed += Time.deltaTime;
            thisHorse.position = thisHorse.position - new Vector3(1f, 1f, 0f);
        }

        //Gain horse back and horse disappears?
        currentAmmo = 1;
    }
}
