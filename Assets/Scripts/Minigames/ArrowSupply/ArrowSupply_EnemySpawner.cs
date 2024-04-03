using UnityEngine;

public class ArrowSupply_EnemySpawner : MonoBehaviour
{
    [SerializeField] Character characterPrefab;
    [SerializeField] CharacterModel.Config[] configs;
    private bool isFirstSpawn = true;
    private float initialSpawnDelay = 1f; // Delay for the first spawn
    [SerializeField] float minSpawnDelay = 5f; // New minimum time between spawns after the first spawn
    [SerializeField] float maxSpawnDelay = 10f; // New maximum time between spawns after the first spawn
    private float timer;

    // Reference to the ArrowSupplyMatch script
    private ArrowSupplyMatch match => (ArrowSupplyMatch)Game.Match;

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
            // Check if the match is not in the postMatch state before spawning enemies
            if (match.Mode != ArrowSupplyMatch.EState.postMatch)
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
    }

    public void SpawnEnemy()
    {
        if (configs.Length == 0) return;

        Character character = Instantiate(characterPrefab, transform.position, transform.rotation);

        CharacterModel.Config config = configs[Random.Range(0, configs.Length)];

        // Debug.Log("New config = " + config.Variant.ToString());

        character.SetToEnemyInArrowSupply();

        character.Model.SetNewConfig(config);

        character.SetNewState(new CS_ArrowSupply_EnemyLocomotion(character));

        // Add EnemyHealth script to the spawned enemy
        EnemyHealth enemyHealth = character.gameObject.AddComponent<EnemyHealth>();
    }

    float randomSpawnDelay => Random.Range(minSpawnDelay, maxSpawnDelay);
}
