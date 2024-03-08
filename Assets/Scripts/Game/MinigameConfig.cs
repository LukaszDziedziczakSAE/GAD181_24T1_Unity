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
        SceneManager.LoadScene(BuildIndex);
    }
}
