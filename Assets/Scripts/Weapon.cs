using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private GameObject currentCharacter; //cowboy character that is shooting from
    public GameObject bullet;
    public float maxAmmo;
    private float currentAmmo;
    

    // Start is called before the first frame update
    void Start()
    {
        //this switches based on turns fix later
        currentCharacter = GameObject.Find("TestCharacter");
        //this also switches based on current character
        // maybe just turn visibility on and off or something
        //change bullet stats?
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        //ROTATING
        if (Input.GetKey("up"))
        {
            //rotate up around gun
            transform.RotateAround(this.transform.Find("Gun").transform.position, Vector3.forward, 100 * Time.deltaTime);
            //clamp rotation
            //transform.rotation.y = Mathf.Clamp(transform.eulerAngles.y, -45, 45);
        }
        if (Input.GetKey("down"))
        {
            //rotate weapon down around gun
            transform.RotateAround(this.transform.Find("Gun").transform.position, Vector3.forward, -100 * Time.deltaTime);
            //transform.rotation.y = Mathf.Clamp(transform.eulerAngles.y, -45, 45);
        }
        // RELOADING
        if (Input.GetKey("r"))
        {
            currentAmmo = maxAmmo;
        }
        //SHOOTING 
        // enter for now
        if (Input.GetKeyDown(KeyCode.Return) && currentAmmo != 0)
        {
            Debug.Log("Shooting");
            GameObject shotBullet = Instantiate(bullet, this.transform.Find("BulletSpawn").position, this.transform.rotation);
            currentAmmo--;
        }
    }
}
