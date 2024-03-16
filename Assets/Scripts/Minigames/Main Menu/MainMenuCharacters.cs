using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.UIElements.ToolbarMenu;

public class MainMenuCharacters : MonoBehaviour
{
    [SerializeField] float spawnRadius;
    [SerializeField] Character characterPrefab;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float characterProximity;

    [field: SerializeField] public List<Character> Characters = new List<Character>();

    private void Update()
    {
        if (Characters.Count < charactersToSpawn)
        {

            if (Characters.Count < Game.ConfigsUnlockedAt(0).Length)
            {
                Character character = Spawn(Game.ConfigsUnlockedAt(0)[Characters.Count]);
                if (character != null) Characters.Add(character);
            }
            else
            {
                Character character = Spawn(Game.Player.Currency.
                    PurchasedCharacters[Characters.Count - Game.ConfigsUnlockedAt(0).Length]);
                if (character != null) Characters.Add(character);
            }
        }
    }

    public void ClearCharacters()
    {
        foreach (Character character in Characters)
        {
            Destroy(character.gameObject);
        }
        Characters.Clear();
    }

    public void SpawnCharacters()
    {
        if (Characters.Count > 0) ClearCharacters();

        foreach (CharacterConfig config in Game.ConfigsUnlockedAt(0))
        {
            Character character = Spawn(config);
            if (character != null) Characters.Add(character);
        }

        foreach (CharacterModel.EVariant variant in Game.Player.Currency.PurchasedCharacters)
        {
            Character character = Spawn(variant);
            if (character != null) Characters.Add(character);
        }
    }

    private int charactersToSpawn => Game.ConfigsUnlockedAt(0).Length + Game.Player.Currency.PurchasedCharacters.Count;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    private Character Spawn(CharacterConfig config)
    {
        Vector3 position = NextSpawnPoint;
        if (position == new Vector3()) return null;
        Character character = Instantiate(characterPrefab, position, Quaternion.identity);
        character.transform.parent = transform;
        character.Model.SetVariant(config.Variant);
        character.name = config.Variant.ToString();
        character.SetNewState(new CS_MainMenu_Wondering(character));
        return character;
    }

    private Character Spawn(CharacterModel.EVariant variant)
    {
        Vector3 position = NextSpawnPoint;
        if (position == new Vector3()) return null;
        Character character = Instantiate(characterPrefab, position, Quaternion.identity);
        character.transform.parent = transform;
        character.Model.SetVariant(variant);
        character.name = variant.ToString();
        character.SetNewState(new CS_MainMenu_Wondering(character));
        return character;
    }

    public Vector3 NextSpawnPoint
    {
        get
        {
            Vector3 spawnPoint = new Vector3();
            Vector3 postion = randomPosition;
            if (postion != new Vector3() && !NearAnotherCharacter(postion))
            {
                spawnPoint = postion;
            }
            return spawnPoint;
        }
    }

    private Vector3 randomPosition
    {
        get
        {
            float degrees = Random.Range(0f, 360f);
            float radians = degrees * Mathf.Deg2Rad;
            float x = (Mathf.Cos(radians) * spawnRadius) * Random.Range(0f, 1f);
            float y = 500;
            float z = (Mathf.Sin(radians) * spawnRadius) * Random.Range(0f, 1f);
            Vector3 position = new Vector3(transform.position.x + x, y, transform.position.z + z);

            if (Physics.Raycast(position, new Vector3(0, -1 ,0), out RaycastHit hit, 1000f, groundMask))
            {
                return hit.point;
            }
            Debug.LogWarning("no hit " + position.ToString());
            return new Vector3();
        }
    }

    private bool NearAnotherCharacter(Vector3 position)
    {
        foreach (Character character in Characters)
        {
            float distance = Vector3.Distance(character.transform.position, position);
            if (distance <= characterProximity)
            {
                return true;
            }
        }
        return false;
    }
}
 