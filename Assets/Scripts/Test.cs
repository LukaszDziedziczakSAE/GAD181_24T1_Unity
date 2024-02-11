using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Update()
    {
        if (text == null) return;

        text.text = "" + Game.InputReader.TouchPosition.ToString();
    }
}
