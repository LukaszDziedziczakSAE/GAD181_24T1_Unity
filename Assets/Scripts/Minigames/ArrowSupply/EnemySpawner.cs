using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    /*public GameObject[] enemyPrefabs; // Array to hold enemy prefabs

    public Transform spawnPoint; // Point where enemies will spawn

    public float minSpawnDelay = 1f; // Minimum time between spawns

    public float maxSpawnDelay = 3f; // Maximum time between spawns

    void Start()
    {
        // Call the SpawnEnemy method repeatedly with a delay
        Invoke("SpawnEnemy", Random.Range(minSpawnDelay, maxSpawnDelay)); // Spawn the first enemy with a random delay
    }

    void SpawnEnemy()
    {
        // Randomly select an enemy prefab from the array
        int randomIndex = Random.Range(0, enemyPrefabs.Length);

        GameObject enemyPrefab = enemyPrefabs[randomIndex];

        // Spawn the selected enemy at the spawn point
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Call SpawnEnemy again with a new random delay
        Invoke("SpawnEnemy", Random.Range(minSpawnDelay, maxSpawnDelay));
    }*/

    [SerializeField] Character characterPrefab;
    [SerializeField] CharacterModel.Config[] configs;
    [SerializeField] float minSpawnDelay = 1f; // Minimum time between spawns
    [SerializeField] float maxSpawnDelay = 3f; // Maximum time between spawns

    float timer;

    private void Start()
    {
        timer = randomSpawnDelay;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnEnemy();
            timer = randomSpawnDelay;
        }
    }

    public void SpawnEnemy()
    {
        if (configs.Length == 0) return;

        Character character = Instantiate(characterPrefab, transform.position, transform.rotation);
        CharacterModel.Config config = configs[Random.Range(0, configs.Length)];
        //Debug.Log("New config = " + config.Variant.ToString());
        character.SetToEnemyInArrowSupply();
        character.Model.SetNewConfig(config);
        character.SetNewState(new CS_ArrowSupply_EnemyLocomotion(character));
    }

    float randomSpawnDelay => Random.Range(minSpawnDelay, maxSpawnDelay);
}