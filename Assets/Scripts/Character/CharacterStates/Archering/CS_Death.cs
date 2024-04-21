using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CS_Death : CharacterState
{

    bool isAlive = true;


    public CS_Death(Character character) : base(character)
    {
    }
    public override void StateStart()
    {
        if (!isAlive) return;
        RandomAnimation(random);
    }

    public override void Tick()
    {

    }
    public override void FixedTick()
    {

    }

    public override void StateEnd()
    {

    }


    void RandomAnimation(int random)//how to add bool to individual characters
    {
        
        if (random == 1)
        {
            character.Animator.CrossFade("Dying_Backwards", 0.1f);
            isAlive = false;
        }
        else if (random == 2)
        {
            character.Animator.CrossFade("Falling_Back_Death", 0.1f);
            isAlive = false;
        }
        else if (random == 3)
        {
            character.Animator.CrossFade("Flying_Back_Death", 0.1f);
            isAlive = false;
        }
        
    }

    int random
    {

        get
        {
            return Random.Range(0, 3);
        }
    }

}
