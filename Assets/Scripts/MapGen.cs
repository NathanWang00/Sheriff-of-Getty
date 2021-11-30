using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGen : MonoBehaviour
{
    public int width;

    public int height;

    public GameObject block;

    public GameObject[] allies;

    public GameObject[] enemies;

    public float ledgeChance;

    public float coverChance;

    public float coverHeightChance;
    
    private static GameManager gameManager;

    // Start is called before the first frame update
    private void Start()
    {
        Generation();
        AddEntities(allies);
        AddEntities(enemies);
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        gameManager.ToggleStart();
    }

    private void AddEntities(GameObject[] entities)
    {
        // allies
        foreach (var entity in entities)
        {
            var chanceToSpawn = 0.05;
            var hasSpawned = false;
            while (!hasSpawned)
            {
                for (var j = -width + 1; j < width; j++)
                {
                    if (Random.value < chanceToSpawn)
                    {
                        var location = new Vector2(j, -height + 1);
                        var checkOpen  = Physics2D.OverlapCircle(location, 0.1f);
                        if (checkOpen == null)
                        {
                            Instantiate(entity, location, Quaternion.identity);
                            hasSpawned = true;
                        }
                    }
                    if (hasSpawned) break;
                } 
                if (hasSpawned) break;
                for (var j = -width + 1; j < width; j++)
                {
                    if (Random.value < chanceToSpawn)
                    {
                        var location = new Vector2(j, 1);
                        var checkOpen  = Physics2D.OverlapCircle(location, 0.1f);
                        var checkBelow = Physics2D.OverlapCircle(location + Vector2.down, 0.1f);
                        if (checkOpen == null && checkBelow != null)
                        {
                            Instantiate(entity, location, Quaternion.identity);
                            hasSpawned = true;
                        }
                    }
                    if (hasSpawned) break;
                }
            }
        }
    }
    private void Generation()
    {
        var horizontalMovement = -width;
        while (Random.value < ledgeChance && horizontalMovement < 0)
        {
            Instantiate(block, new Vector2(horizontalMovement, 0), Quaternion.identity);
            horizontalMovement++;
        }

        horizontalMovement = width;
        while (Random.value < ledgeChance && horizontalMovement >= 0)
        {
            Instantiate(block, new Vector2(horizontalMovement, 0), Quaternion.identity);
            horizontalMovement--;
        }
        
        // cover gen for ground 
        for (var i = -width + 1; i < width; i++)
        {
            if (Random.value < coverChance)
            {
                var spawn = new Vector2(i, -height + 1);
                var checkLeft = Physics2D.Raycast(spawn, Vector2.left, 3);
                var checkRight = Physics2D.Raycast(spawn, Vector2.right, 3);
                if (checkLeft.collider == null && checkRight.collider == null) GenerateCover(spawn);
            }
        }
        
        // & ledges
        for (var i = -width + 1; i < width; i++)
        {
            if (Random.value < coverChance)
            {
                var location = new Vector2(i, 1);
                var hit = Physics2D.Raycast(location, Vector2.down, 1);
                if (hit.collider != null) GenerateCover(location);
            }
        }

    }

    private void GenerateCover(Vector2 location)
    {
        for (var i = 0; i < 2; i++)
        {
            Instantiate(block, new Vector2(location.x, location.y + i), Quaternion.identity);
        }
    }

    private bool isObjectHere(Vector2 location)
    {
        Collider[] intersecting = Physics.OverlapSphere(location, 0.01f);
        return (intersecting.Length == 0);
    }
    
}
