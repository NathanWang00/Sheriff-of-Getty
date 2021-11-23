using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This should handle all horsin all behavior
public class Horse : MonoBehaviour
{
    private GameObject currentCharacter; //character that can ride horse
    private bool beingRidden;
    // Start is called before the first frame update
    void Start()
    {
        //only this cowboy can ride this horse. they're best friends and can never be seperated
        currentCharacter = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //MOUNTING
            if(Input.GetKeyDown("enter")){
            float distance = Vector3.Distance(this.transform.position, currentCharacter.transform.position);
                //cowboy has to be right next to horse to mount
                if(distance < 0.5f && !beingRidden){
                    Vector3 seatedPosition = new Vector3(currentCharacter.transform.position.x, currentCharacter.transform.position.y + 0.5f, 0);
                    currentCharacter.transform.position = seatedPosition;
                    currentCharacter.transform.parent = this.transform;
                    beingRidden = true;
                }
                //detach cowboy from horse
                else if(beingRidden){
                    currentCharacter.transform.parent = null;
                    beingRidden = false;
                }
        }
        //RIDING
        //based on player movement, so implement later
    }
}
