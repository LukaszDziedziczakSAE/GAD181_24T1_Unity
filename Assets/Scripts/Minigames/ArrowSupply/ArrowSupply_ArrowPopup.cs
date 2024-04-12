using UnityEngine;
using UnityEngine.UI;

public class ArrowSupply_ArrowPopup : MonoBehaviour
{
    public RawImage iconImage;  // Reference to the UI image component

    public Texture normalIcon;  // Texture for normal enemy
    public Texture iceIcon;     // Texture for ice enemy
    public Texture fireIcon;    // Texture for fire enemy

    // Method to update the icon based on enemy type
    public void UpdateIcon(CharacterModel.EVariant enemyType)
    {
        switch (enemyType)
        {
            case CharacterModel.EVariant.Dungeon_GoblinMale_01:
                iconImage.texture = normalIcon;
                break;
            case CharacterModel.EVariant.Dungeon_RockGolem_01:
                iconImage.texture = iceIcon;
                break;
            case CharacterModel.EVariant.Dungeon_Skeleton_01:
                iconImage.texture = fireIcon;
                break;
            default:
                Debug.LogError("Unknown enemy type");
                break;
        }

        // Show the UI element (assuming it's hidden by default)
        gameObject.SetActive(true);
    }
}