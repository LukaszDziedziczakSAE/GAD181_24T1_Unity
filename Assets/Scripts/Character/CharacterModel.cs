using System;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    [field: SerializeField] public CharacterModel.Config CurrentConfig {  get; private set; }
    [field: SerializeField] public SkinnedMeshRenderer CurrentMeshRenderer { get; private set; }
    [SerializeField] CharacterModelPair[] CharacterModelPairs;
    [SerializeField] Material[] defaultColorMaterials;
    [SerializeField] Material[] blueColorMaterials;
    [SerializeField] Material[] greenColorMaterials;
    [SerializeField] Material[] purpleColorMaterials;
    [SerializeField] Material[] redColorMaterials;
    [SerializeField] Material[] yellowColorMaterials;


    public enum EVariant
    {
        Random,
        Adventure_Peasant_01,
        Adventure_ShopKeeper_01,
        Adventure_Viking_01,
        Adventure_Warrior_01,
        Dungeon_GoblinChief_01,
        Dungeon_GoblinFemale_01,
        Dungeon_GoblinMale_01,
        Dungeon_GoblinShaman_01,
        Dungeon_GoblinWarriorFemale_01,
        Dungeon_GoblinWarriorMale_01,
        Dungeon_KnightFemale_01,
        Dungeon_KnightMale_01,
        Dungeon_RockGolem_01,
        Dungeon_Skeleton_01,
        Dungeon_SkeletonKnight_01,
        Dungeon_SkeletonSoldier_01,
        Dungeon_SkeletonSoldier_02,
        Fantasy_Bard_01,
        Fantasy_Druid_01,
        Fantasy_FemalePeasant_01,
        Fantasy_Gypsy_01,
        Fantasy_King_01,
        Fantasy_MalePeasant_01,
        Fantasy_PeasantWoman_01,
        Fantasy_Queen_01,
        Fantasy_RougeMale_01,
        Fantasy_Sorcerer_01,
        Fantasy_Witch_01,
        Fantasy_Wizard_01,
        Knights_Dark_01,
        Knights_Light_01,
        Kights_Soldier_01,
        Pirates_Deckhand_01,
        Pirates_FemalePirate_01,
        Pirates_Firstmate_01,
        Pirates_Gentleman_01,
        Pirates_GovDaughter_01,
        Samurai_Geisha_01,
        Samurai_Grunt_01,
        Samurai_SamuraiWarrior_01,
        Samurai_Sensei_01,
        Samurai_VillageMan_01,
        Samurai_VillageWorman_01,
        Vikings_Chief_01,
        Vikings_ShieldMaiden_01,
        Vikings_Warrior_01,
        Vikings_Warrior_02
    }

    public enum EColor
    {
        Default,
        Blue,
        Green,
        Purple,
        Red,
        Yello
    }

    [System.Serializable]
    public struct CharacterModelPair
    {
        [field: SerializeField] public EVariant Variant { get; private set; }
        [field: SerializeField] public GameObject Model { get; private set; }
    }

    [System.Serializable]
    public class Config
    {
        [field: SerializeField] public EVariant Variant { get; private set; }
        [field: SerializeField, Range(0,2)] public int Skin { get; private set; }
        [field: SerializeField] public EColor Color { get; private set; }

        public Config(EVariant variant, int skinIndex, EColor color)
        {
            Variant = variant;
            Skin = skinIndex;
            Color = color;
        }
    }

    public void HideAllModels()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name == "root" || child.name == "Model") continue;

            child.gameObject.SetActive(false);
        }
    }

    public void SetVariant(EVariant variant)
    {
        HideAllModels();

        if (variant == EVariant.Random)
        {
            int random = UnityEngine.Random.Range(0, CharacterModelPairs.Length);
            CharacterModelPairs[random].Model.SetActive(true);
            CurrentMeshRenderer = CharacterModelPairs[random].Model.GetComponent<SkinnedMeshRenderer>();
        }

        else
        {
            foreach (CharacterModelPair pair in CharacterModelPairs)
            {
                if (pair.Variant == variant)
                {
                    pair.Model.SetActive(true);
                    CurrentMeshRenderer = pair.Model.GetComponent<SkinnedMeshRenderer>();
                }
            }
        }
        

    }

    public void SetMaterial(Material material)
    {
        if (CurrentMeshRenderer == null) return;
        CurrentMeshRenderer.material = material;
    }

    public void SetNewConfig(Config newConfig)
    {
        CurrentConfig = newConfig;
        SetVariant(CurrentConfig.Variant);
        SetMaterial(MaterialByColors(CurrentConfig.Skin, CurrentConfig.Color));
    }

    public Material MaterialByColors(int skin, EColor color)
    {
        switch(color)
        {
            case EColor.Default:
                return defaultColorMaterials[skin];

            case EColor.Blue:
                return blueColorMaterials[skin];

            case EColor.Green:
                return greenColorMaterials[skin];

            case EColor.Purple:
                return purpleColorMaterials[skin];

            case EColor.Red:
                return redColorMaterials[skin];

            case EColor.Yello:
                return yellowColorMaterials[skin];

            default:
                Debug.LogError("Could not find Material " + skin + " " + color.ToString());
                return null;
        }
    }

    public static EVariant VariantName(string name)
    {
        Type enumType = CharacterModel.EVariant.Random.GetType();
        for (int i = 0; i < Enum.GetNames(enumType).Length && i < 100; i++)
        {
            if (Enum.GetName(enumType, i) == name) return (EVariant)i;
        }

        Debug.LogError("Could not find EVariant = " + name);
        return (EVariant)0;
    }
}
