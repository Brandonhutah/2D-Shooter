using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Times")]
    [Tooltip("Initial time between enemy spawns")]
    public float initialSpawnInterval = 10;
    [Tooltip("Minimum time between enemy spawns")]
    public float minimumSpawnInterval = 1f;
    [Tooltip("Step size when decrementing spawn time")]
    public float spawnTimeStep = 0.2f;
    [Tooltip("Time between spawn time decrementing")]
    public float spawnStepInterval = 5;

    [Tooltip("Enemy prefabs that will be randomly chosen from for each spawn")]
    public List<GameObject> enemyPrefabs;
    [Tooltip("Empty object to place newly spawned enemies in the keep hierarchy clean")]
    public Transform enemyHolder;


    private GameObject[] spawnLocations;
    private bool decrementInterval = true;
    private float currentSpawnInterval;
    private float lastSpawnTime = Mathf.NegativeInfinity;
    private float lastIntervalStep = Mathf.NegativeInfinity;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        spawnLocations = GameObject.FindGameObjectsWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameIsOver)
        {
            return;
        }

        if (Time.timeSinceLevelLoad > lastSpawnTime + currentSpawnInterval)
        {
            SpawnEnemy(GetEnemyToSpawn());
            lastSpawnTime = Time.timeSinceLevelLoad;
        }

        if (decrementInterval && Time.timeSinceLevelLoad > lastIntervalStep + spawnStepInterval)
        {
            if (currentSpawnInterval - spawnTimeStep < minimumSpawnInterval)
            {
                currentSpawnInterval = minimumSpawnInterval;
                decrementInterval = false;
            } else
            {
                currentSpawnInterval = currentSpawnInterval - spawnTimeStep;
            }

            lastIntervalStep = Time.timeSinceLevelLoad;
        }
    }

    private GameObject GetEnemyToSpawn()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
    }

    private void SpawnEnemy(GameObject enemyToSpawn)
    {
        // Make sure the prefab is valid
        if (enemyToSpawn != null)
        {
            int spawner = Random.Range(0, spawnLocations.Length);

            // Create the enemy gameobject
            GameObject enemy = Instantiate(enemyToSpawn, spawnLocations[spawner].transform.position, spawnLocations[spawner].transform.rotation, null);

            // Keep the heirarchy organized
            if (enemyHolder != null)
            {
                enemy.transform.SetParent(enemyHolder);
            }
        }
    }
}
