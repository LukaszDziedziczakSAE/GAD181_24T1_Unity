using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
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

        // Add EnemyHealth script to the spawned enemy
        EnemyHealth enemyHealth = character.gameObject.AddComponent<EnemyHealth>();            

    }

    float randomSpawnDelay => Random.Range(minSpawnDelay, maxSpawnDelay);
}