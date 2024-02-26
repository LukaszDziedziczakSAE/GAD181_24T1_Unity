using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MatchEnd : MonoBehaviour
{
    [SerializeField] Button backgroundButton;
    [SerializeField] Button playAgainButton;
    [SerializeField] Button mainMenuButton;

    [SerializeField] GameObject matchEndListPrefab;
    [SerializeField] Transform matchEndList;
    [SerializeField] TMP_Text playerCurrency;
    [SerializeField] TMP_Text playerCurrencyErned;
    [SerializeField] TMP_Text playerExperience;
    [SerializeField] TMP_Text playerExperienceErned;
}
