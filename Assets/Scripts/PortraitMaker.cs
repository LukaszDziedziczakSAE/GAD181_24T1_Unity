using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PortraitMaker : MonoBehaviour
{
    float rate = 0.1f;
    float timer;
    int count = 1;
    bool finished;
    int max;

    private void Start()
    {
        Game.PlayerCharacter.Model.SetVariant((CharacterModel.EVariant)count);
        timer = rate;
        max = Game.PlayerCharacter.Model.Configs.Length;
    }

    private void Update()
    {
        

        timer -= Time.deltaTime;
        if (timer < 0 )
        {
            if (finished)
            {
                Game.LoadMainMenu();
            }

            TakePortrait();
            timer = rate;
            count++;
            if (count > max)
            {
                finished = true;
                Debug.LogWarning("Portrait Making complete");
                
            }
            
        }
    }

    private void TakePortrait()
    {
        CharacterModel.EVariant varient = (CharacterModel.EVariant)count;
        Game.PlayerCharacter.Model.SetVariant(varient);
        string fileName = "Assets\\Textures\\" + varient.ToString() + ".png";
        ScreenCapture.CaptureScreenshot(fileName);
        Debug.Log("saved " + fileName);
        
    }
}
