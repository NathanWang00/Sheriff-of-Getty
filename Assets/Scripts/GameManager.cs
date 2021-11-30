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
    // public GameObject[] worms = new GameObject[5];
    public List<GameObject> worms = new List<GameObject>();
    public Queue turnOrder = new Queue();
    public int playersRemain = 0;
    public int enemiesRemain = 0;

    private void Awake()
    {
        _instance = this;
    }

    private void Start() {
        // sets a number of players and enemies
        playersRemain = 3;
        enemiesRemain = 2;

        GameObject[] wormsArray = new GameObject[playersRemain + enemiesRemain];
        wormsArray = GameObject.FindGameObjectsWithTag("Character");

        for(int i = 0; i < wormsArray.Length; i++) {
            worms.Add(wormsArray[i]);
        }

        SetTurnOrder();
        NextTurn();
    }

    private void Update() {
        GameOverMessage();
        // Switches turn based on user input
        // TO SWITCH TURNS, PRESS "T"

        if(Input.GetKeyDown(KeyCode.T)) {
            // If each character exhausted their turn, reset the round
            if(turnOrder.Count <= 0) SetTurnOrder();
            // Sets the next player's turn
            NextTurn();
        }

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

    // Sets the next character's turn and sets every other character that is
    // not their turn to false as to avoid any possible simultaneous play
    public void NextTurn() {
        if(turnOrder.Count <= 0) return;

        int chosen = (int)turnOrder.Dequeue();
        Debug.Log("Next Turn: " + chosen);

        // if the chosen one next in line is no longer in the queue, let player know that character is DEAD
        if(chosen >= worms.Count && turnOrder.Count > 0) {
            Debug.Log("CHARACTER IS DEAD");
            return;
        }

        for(int i = 0; i < worms.Count; i++) {
            if(i == chosen) worms[i].GetComponent<Character>().SelectCharacter(true);
            else worms[i].GetComponent<Character>().SelectCharacter(false);
        }
    }

    // Resets every turn to have a fresh start. Chooses order randomly
    private void SetTurnOrder() {
        Debug.Log("Set New Turn Order for Round");
        turnOrder.Clear();

        // switches between player and enemy characters on field. "false" -> player, "true" -> enemy
        string characterTypePosition = "Player";
        while(turnOrder.Count < worms.Count) {
            int nextIndex = Random.Range(0, worms.Count);
            // sets player if available and in position
            if(!turnOrder.Contains(nextIndex)) {
                // when the current one is player type
                // if(worms[nextIndex].GetComponent<Character>().characterType == characterTypePosition) {
                if(characterTypePosition == "Player" && worms[nextIndex].GetComponent<Character>().characterType == "Player") {
                    turnOrder.Enqueue(nextIndex);
                    characterTypePosition = "Enemy";

                // when the current one is enemy type
                } else if(characterTypePosition == "Enemy" && worms[nextIndex].GetComponent<Character>().characterType == "Enemy") {
                    turnOrder.Enqueue(nextIndex);
                    characterTypePosition = "Player";
                }
            }
        }

        // DEBUG PURPOSES
        // for(int i = 0; i < worms.Length; i++) {
        //     int temp = (int)turnOrder.Dequeue();
        //     Debug.Log("Type: " + worms[temp].GetComponent<Character>().characterType + " #" + i);
        //     turnOrder.Enqueue(temp);
        // }
    }

    // sends out a message to see if one team remains victory
    private void GameOverMessage() {
        if(playersRemain <= 0) {
            Debug.Log("ENEMY TEAM WINS");
            StartCoroutine("DisplayText", "ENEMY WINS");
        }
        if(enemiesRemain <= 0) {
            Debug.Log("PLAYER TEAM WINS");
            StartCoroutine("DisplayText", "PLAYER WINS");
        }
    }
}
