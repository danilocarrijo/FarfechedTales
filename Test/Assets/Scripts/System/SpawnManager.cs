using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private bool hasSpawed;
    private Transform player;

    public GameObject[] enemiesToSpawn;

    public int maxSpawnAmount;

    public float spawnRange;
    public float detectionRange;

    private void Start()
    {
        player = PlayerManager.instance.ourPlayer.transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance < detectionRange && !hasSpawed)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        hasSpawed = true; 

        int spawnAmount = Random.Range(1, maxSpawnAmount);

        for (int i = 0; i < spawnAmount; i++)
        {
            float xSpawnPos = transform.position.x + Random.Range(-spawnRange,spawnRange);
            float zSpawnPos = transform.position.z + Random.Range(-spawnRange, spawnRange);

            Vector3 spawnPoint = new Vector3(xSpawnPos,0,zSpawnPos);
            GameObject newEnemy = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)],spawnPoint,Quaternion.identity) as GameObject;
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.blue;

        Handles.DrawWireArc(transform.position, transform.up, transform.right, 360, detectionRange);

        Handles.color = Color.green;

        Handles.DrawWireArc(transform.position, transform.up, transform.right, 360, spawnRange);
    }

}
