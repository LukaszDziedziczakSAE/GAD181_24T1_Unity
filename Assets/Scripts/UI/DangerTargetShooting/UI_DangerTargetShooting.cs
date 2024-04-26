using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DangerTargetShooting : UI_Main
{
    [field: SerializeField] public UI_ArrowShootingIndicator ArrowShootingIndicator { get; private set; }
    [field: SerializeField] public UI_ArrowIndicator ArrowIndicator { get; private set; }
    [field: SerializeField] public UI_PowerSlider PowerSlider { get; private set; }
    [field: SerializeField] public UI_Crosshair Crosshair { get; private set; }

    public override void LevelLoaded()
    {

    }
}
