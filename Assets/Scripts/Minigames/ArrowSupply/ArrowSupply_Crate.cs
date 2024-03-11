using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupply_Crate : MonoBehaviour
{
    /*public GameObject arrowPrefab; // Reference to the arrow prefab

    private bool isArrowCollected = false; // Flag to track if an arrow has been collected from this crate

    private void OnTriggerEnter(Collider other)
    {
        if (!isArrowCollected && other.CompareTag("Player"))
        {
            CollectArrow(other.gameObject);
        }
    }

    private void CollectArrow(GameObject player)
    {
        
        GameObject arrow = Instantiate(arrowPrefab, player.transform.position, Quaternion.identity);// Instantiate arrow at player's position and rotation

        arrow.transform.parent = transform;

        isArrowCollected = true; // Mark the arrow as collected

    }*/

    [SerializeField] ArrowSupply_Arrow arrowPrefab;
    [SerializeField] ArrowSupply_Arrow.EType arrowType;
    [SerializeField] Transform previewSpot;
    [SerializeField] float previewRotationSpeed = 30;
    [SerializeField] Vector3 localHandPosition;
    [SerializeField] Vector3 localHandRotation;

    ArrowSupply_Arrow previewArrow;
    public ArrowSupply_Arrow.EType ArrowType => arrowType;
    public ArrowSupply_Arrow ArrowPrefab => arrowPrefab;

    private void Start()
    {
        previewArrow = Instantiate(arrowPrefab, previewSpot);
        previewArrow.SetType(arrowType);
    }

    private void Update()
    {
        if (previewArrow != null) previewArrow.transform.Rotate(Vector3.up * previewRotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(name + " has " + other.name + " in trigger");
        Character character = other.GetComponent<Character>();
        if (character != null && character.PlayerIndex >= 0 && character.PlayerIndex < 100)
        {
            character.SetNewState(new CS_ArrowSupply_PickUp(character, this));
        }
    }

    public ArrowSupply_Arrow SpawnInCharactersHand(Character character)
    {
        ArrowSupply_Arrow arrow = Instantiate(arrowPrefab, character.RightHand);
        arrow.SetType(arrowType);
        arrow.transform.localPosition = localHandPosition;
        arrow.transform.localEulerAngles = localHandRotation;
        arrow.transform.localScale = Vector3.one;
        //print("arrow pos=" + arrow.transform.localPosition + " rot=" + arrow.transform.localEulerAngles);

        return arrow;
    }
}
