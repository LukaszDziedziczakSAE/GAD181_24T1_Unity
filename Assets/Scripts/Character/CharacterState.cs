using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState
{
    public abstract void StateStart();
    public abstract void StateEnd();
    public abstract void Tick();
    public abstract void FixedTick();
}
