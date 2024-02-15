using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterModel;

[CreateAssetMenu(menuName = "New CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
    [field: SerializeField] public string CharacterName { get; private set; }
    [field: SerializeField] public EVariant Variant { get; private set; }
    [field: SerializeField] public string PartName { get; private set; }
    [field: SerializeField] public CharacterModel.EColor DefaultColor { get; private set; } 
        = CharacterModel.EColor.Default;
    [field: SerializeField, Range(0, 2)] public int DefaultSkinIndex { get; private set; } = 0;
    [field: SerializeField] public int UnlockLevel { get; private set; } = -1;
    [field: SerializeField] public int UnlockPrice { get; private set; } = 0;
    [field: SerializeField] public Texture Icon { get; private set; }

    public void CreateNew(EVariant variant, string partName)
    {
        Variant = variant;
        PartName = partName;
    }

    public string nameFixed
    {
        get
        {
            string name = this.name;
            name = name.Replace("Adventure_", "");
            //name = name.Replace("Knights_", "");
            name = name.Replace("Fantasy_", "");
            name = name.Replace("Samurai_", "");
            name = name.Replace("Dungeon_", "");
            name = name.Replace("Pirates_", "");
            name = name.Replace("_01", "");
            name = name.Replace("_02", "2");
            return name;
        }
    }

    public void SetIcon(Texture newTexture)
    {
        Icon = newTexture;
    }
}
