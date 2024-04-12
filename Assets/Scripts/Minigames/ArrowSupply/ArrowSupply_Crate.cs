using UnityEngine;

public class ArrowSupply_Crate : MonoBehaviour
{
    [SerializeField] ArrowSupply_Arrow arrowPrefab;

    [SerializeField] ArrowSupply_Arrow.EType arrowType;

    [SerializeField] Transform previewSpot;

    [SerializeField] float previewRotationSpeed = 30;

    [SerializeField] Vector3 localHandPosition;

    [SerializeField] Vector3 localHandRotation;

    ArrowSupply_AI aiController;

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
        if (previewArrow != null)

            previewArrow.transform.Rotate(Vector3.up * previewRotationSpeed * Time.deltaTime);
    }

    public void SetAIController(ArrowSupply_AI controller)
    {
        aiController = controller;
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character != null && character.PlayerIndex >= 0 && character.PlayerIndex < 100 && character.State.GetType() == new CS_ArrowSupply_Locomotion(character).GetType())
        {
            // Set the character's state to pick up the arrow
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

        //Debug.Log(character.PlayerIndex + " has picked up " + arrow);

        return arrow;
    }
}