using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] float showTime;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= showTime)
        {
            Game.LoadMainMenu();
        }
    }

    public void Skip()
    {
        timer = showTime;
    }
}
