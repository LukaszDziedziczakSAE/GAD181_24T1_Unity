using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour, ISaveable
{
    [field: SerializeField] public int AmountHeld { get; private set; }
    [field: SerializeField] public List<CharacterModel.EVariant> PurchasedCharacters { get; private set; } = new List<CharacterModel.EVariant>();


    public object CaptureState()
    {
        Dictionary<string, object> state = new Dictionary<string, object>();

        state.Add("AmountHeld", AmountHeld);
        state.Add("PurchasedCharacters", PurchasedCharacters);

        return state;
    }

    public void RestoreState(object state)
    {
        Dictionary<string, object> restoredState = (Dictionary<string, object>)state;

        AmountHeld = (int)restoredState["AmountHeld"];
        PurchasedCharacters = (List<CharacterModel.EVariant>)restoredState["PurchasedCharacters"];
    }

    public void AddCurrency(int amount)
    {
        AmountHeld += amount;
    }

    public void UnlockCharacter(CharacterConfig config)
    {
        AmountHeld -= config.UnlockPrice;
        PurchasedCharacters.Add(config.Variant);
    }

    public bool CanAffordCharacter(CharacterConfig config)
    {
        return AmountHeld >= config.UnlockPrice;
    }

    public bool CharacterIsUnlocked(CharacterModel.EVariant variant)
    {
        return PurchasedCharacters.Contains(variant);
    }
}
