using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This should handle all horsin all behavior
public class Horse : MonoBehaviour
{
    private GameObject currentCharacter; 
    private Rigidbody2D rb;
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
        currentCharacter = this.transform.parent.gameObject;
        currentAmmo = 1;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
            //this.transform.parent = null;
            float power = chargeTime;
            StartCoroutine("BoomerangHorse", power);
        }
    }

    private IEnumerator BoomerangHorse(float power){
        if(this.isShooting){
            yield break;
        }
        Debug.Log("Is shooting");
        //Lose horse
        isShooting = true;
        currentAmmo =0;
        float timer = power * 2;
        Debug.Log("Timer: " + timer);
        Transform thisHorse = this.GetComponent<Transform>();
        //move and spin in certain direction
        rb.velocity = new Vector2(25f, 25f);
        for (float t = 0.0f; t < timer; t += Time.deltaTime){
            rb.rotation += 5f;
            yield return null;
        }
        //move back towards player AND SPIN
        rb.velocity = new Vector2(-25f, -25f);
        for (float t = 0.0f; t < timer; t += Time.deltaTime){
            rb.rotation -= 5f;
            yield return null;
        }

        //Gain horse back and horse disappears?
        thisHorse.position = currentCharacter.transform.position;
        isShooting = false;
        rb.velocity = new Vector2(0f, 0f);
        currentAmmo = 1;
        GameManager.Instance.NextTurn();
    }
}