using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TargetShooting : UI_Main
{

    [field: SerializeField] public UI_ArrowShootingIndicator ArrowShootingIndicator { get; private set; }
    [field: SerializeField] public UI_ArrowIndicator ArrowIndicator { get; private set; }


    public override void LevelLoaded()
    {
        
    }

}
