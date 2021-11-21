using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance //Singleton Stuff
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Game Manager is Null");
            }
            return _instance;
        }
    }

    [SerializeField]
    private bool debugMode;

    [SerializeField]
    private GameObject testBullet;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        // Debug stuff
        /*if (debugMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                position.z = 0;
                Instantiate(testBullet, position, Quaternion.Euler(0, 0, 0));
            }
        }*/
    }

    // GameManager handles collisions to keep things clean and since it's a singleton that needs no reference
    public void Damage(Damageable d, float damage, Vector2 hitForce)
    {
        d.Hurt(damage, hitForce);
    }
}
