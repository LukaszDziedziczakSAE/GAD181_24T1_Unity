using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CS_Death : CharacterState
{

    bool isAlive = true;
    //List<string>deathAnimations = new List<string>(); more efficient to do it like this?

    public CS_Death(Character character) : base(character)
    {
    }
    public override void StateStart()
    {
        character.Animator.CrossFade(randomAnimationName, 0.1f);
        isAlive = false;
       
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


    string randomAnimationName//how to add bool to individual characters
    {
        get
        {
            int random = Random.Range(0, 3);

            switch (random)
            {
                case 0: return "Dying_Backwards";
                case 1: return "Falling_Back_Death";
                case 2: return "Flying_Back_Death";
                default: return string.Empty;
            }
        }
    }

}
