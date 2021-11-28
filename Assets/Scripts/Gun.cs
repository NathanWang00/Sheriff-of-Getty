using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public float maxAmmo;
    private float currentAmmo;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        //change bullet stats?
        currentAmmo = maxAmmo;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ACTIVE VARIABLE
        if(isActive){
            //ROTATING
            if (Input.GetKey("up"))
            {
                //rotate up around gun and clamp rotation
                if(this.transform.rotation.z <= .45){
                    transform.RotateAround(this.transform.Find("Gun").transform.position, Vector3.forward, 100 * Time.deltaTime);
                }
                //transform.rotation += new Vector3(0, Mathf.Clamp(transform.eulerAngles.y, -45, 45), 0);
            }
            if (Input.GetKey("down"))
            {
                //rotate weapon down around gun
                if(this.transform.rotation.z >= -.45){
                    transform.RotateAround(this.transform.Find("Gun").transform.position, Vector3.forward, -100 * Time.deltaTime);
                }
                //transform.rotation = Mathf.Clamp(transform.eulerAngles.y, -45, 45);
            }
            // RELOADING
            if (Input.GetKeyDown("r"))
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
                //TIMER TO END TURN
            }
        }
    }

    private void SetActive(bool active){
        isActive = active;
    }
}
