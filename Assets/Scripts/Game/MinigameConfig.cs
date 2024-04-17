using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "New MinigameConfig")]
public class MinigameConfig : ScriptableObject
{
    [field: SerializeField] public int BuildIndex { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Texture SelectionCardPicture { get; private set; }
    [field: SerializeField] public Texture StartTitleCardPicture { get; private set; }
    [field: SerializeField, Header("Rewards")] public int Xp1stPlace { get; private set; }
    [field: SerializeField] public int Xp2ndPlace { get; private set; }
    [field: SerializeField] public int Xp3rdPlace { get; private set; }
    [field: SerializeField] public int Xp4thPlace { get; private set; }
    [field: SerializeField] public int XpAdditionalPerPoint { get; private set; }
    [field: SerializeField] public int Gold1stPlace { get; private set; }
    [field: SerializeField] public int Gold2ndPlace { get; private set; }
    [field: SerializeField] public int Gold3rdPlace { get; private set; }
    [field: SerializeField] public int Gold4thPlace { get; private set; }
    [field: SerializeField] public int GoldAdditionalPerPoint { get; private set; }

    public void Play()
    {
        //Debug.Log("Starting Leve Load (" + BuildIndex + ")");
        SceneManager.LoadScene(BuildIndex);
    }

    public int GoldAward(int score, int placement)
    {
        switch(placement)
        {
            case 1: return Gold1stPlace + (score * GoldAdditionalPerPoint);
            case 2: return Gold2ndPlace + (score * GoldAdditionalPerPoint);
            case 3: return Gold3rdPlace + (score * GoldAdditionalPerPoint);
            default: return Gold4thPlace + (score * GoldAdditionalPerPoint);
        }
    }

    public int XPAward(int score, int placement)
    {
        switch (placement)
        {
            case 1: return Xp1stPlace + (score * XpAdditionalPerPoint);
            case 2: return Xp2ndPlace + (score * XpAdditionalPerPoint);
            case 3: return Xp3rdPlace + (score * XpAdditionalPerPoint);
            default: return Xp4thPlace + (score * XpAdditionalPerPoint);
        }
    }
}
