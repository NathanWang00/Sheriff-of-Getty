using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoText : MonoBehaviour
{
    public Vector3 Offset;
    public Vector3 OppositeOffset;
    public Text ammoText;
    private float currentAmmo;
    private float maxAmmo;

    // Update is called once per frame
    void Start()
    {
        // get max ammo of current gun
        maxAmmo = gameObject.GetComponentInParent<Gun>().maxAmmo;
    }
    void Update()
    {
        // get current ammo
        currentAmmo = gameObject.GetComponentInParent<Gun>().currentAmmo;
       

        // update ammo text
        if (transform.parent.transform.parent.localScale.x > 0)
        {
            ammoText.transform.position = Camera.main.WorldToScreenPoint(transform.parent.transform.parent.position + Offset);
        }
        else
        {
            ammoText.transform.position = Camera.main.WorldToScreenPoint(transform.parent.transform.parent.position + OppositeOffset);
        }
        
        ammoText.text = currentAmmo + "/" + maxAmmo;
    }
}
