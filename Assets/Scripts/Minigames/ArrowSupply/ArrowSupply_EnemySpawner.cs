using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowSupply_EnemySpawner : MonoBehaviour
{
    [SerializeField] Character characterPrefab;
    [SerializeField] CharacterModel.Config[] configs;
    private bool isFirstSpawn = true;
    private float initialSpawnDelay = 1f; // Delay for the first spawn
    [SerializeField] float minSpawnDelay = 5f; // New minimum time between spawns after the first spawn
    [SerializeField] float maxSpawnDelay = 10f; // New maximum time between spawns after the first spawn

    float timer;

    private void Start()
    {
        // Start with a delay for the initial spawn
        timer = initialSpawnDelay;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnEnemy();
            if (isFirstSpawn)
            {
                // After the first spawn, disable the flag and use the slower spawn rate
                isFirstSpawn = false;
            }
            timer = Random.Range(minSpawnDelay, maxSpawnDelay);
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

        switch (config.Variant)
        {
            case CharacterModel.EVariant.Dungeon_RockGolem_01:
                character.gameObject.AddComponent<ArrowSupply_RockGolemEnemyType>();
                break;
            case CharacterModel.EVariant.Dungeon_Skeleton_01:
                character.gameObject.AddComponent<ArrowSupply_SkeletonEnemyType>();
                break;
            case CharacterModel.EVariant.Dungeon_GoblinMale_01:
                character.gameObject.AddComponent<ArrowSupply_GoblinEnemyType>();
                break;
            default:
                break;
        }
    }

    float randomSpawnDelay => Random.Range(minSpawnDelay, maxSpawnDelay);
}