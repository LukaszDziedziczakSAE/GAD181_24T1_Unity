using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Jousting : UI_Main
{
    [field: SerializeField] public UI_JoustingIndicator JoustingIndicator { get; private set; }
    [field: SerializeField] public UI_JoustingIndicator EnemyJoustingIndicator { get; private set; }

    public override void LevelLoaded()
    {

    }
    
}
