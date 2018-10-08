using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawn : MonoBehaviour {

    private BoxCollider2D triggerColl;
    public GameObject SpawnerPrefab;


    public int numberSpawned;
    public float timeDelayEachSpawn;

    private bool spawnTriggered;

    private void Start()
    {
        triggerColl = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !spawnTriggered)
        {
            spawnTriggered = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        GameObject[] enemies = new GameObject[numberSpawned];
        for (int i = 0; i < numberSpawned; i++)
        {
            enemies[i] = Instantiate(SpawnerPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeDelayEachSpawn);
        }
    }
}
