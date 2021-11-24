using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public float maxAmmo;
    private float currentAmmo;
    //shooting variables
    private Rigidbody2D rb;
    private bool isCharging;
    private float chargeTime;
    private float shootPower;
    //exploding variables
    private bool isExploding;
    private float explodeTime; 
    private float explodeRadius;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        isCharging = false;
        chargeTime = 0;
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ROTATING
        if (Input.GetKey("up"))
        {
            //rotate up around gun and clamp rotation
            if(this.transform.rotation.z <= .45){
                transform.RotateAround(this.transform.position, Vector3.forward, 100 * Time.deltaTime);
            }
            //transform.rotation += new Vector3(0, Mathf.Clamp(transform.eulerAngles.y, -45, 45), 0);
        }
        if (Input.GetKey("down"))
        {
            //rotate weapon down around gun
            if(this.transform.rotation.z >= -.45){
                transform.RotateAround(this.transform.position, Vector3.forward, -100 * Time.deltaTime);
            }
            //transform.rotation = Mathf.Clamp(transform.eulerAngles.y, -45, 45);
        }
        // RELOADING
        if (Input.GetKeyDown("r"))
        {
            currentAmmo = maxAmmo;
        }
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
            float power = chargeTime * 10;
            rb.simulated = true;
            rb.velocity = (transform.right * power);
            chargeTime = 0;
        }
    }
}
