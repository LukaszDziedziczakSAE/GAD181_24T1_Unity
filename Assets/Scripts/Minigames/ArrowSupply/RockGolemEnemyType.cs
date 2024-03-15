using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemEnemyType : MonoBehaviour, EnemyType
{
    public int NormalDamageMultiplier => 2;
    public int FireDamageMultiplier => 1;
    public int IceDamageMultiplier => 4;
}
