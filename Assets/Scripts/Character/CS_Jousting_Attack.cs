using Mono.CSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Jousting_Attack : CharacterState
{
    private JoustingMatch match;
    private Character other;
    private UI_Jousting ui;
    private CS_Jousting_Riding riding;

    public bool playerAttacked = false;

    public CS_Jousting_Attack(Character character) : base(character)
    {
    }

    public override void StateStart()
    {
        Game.InputReader.OnTouchPressed += InputReader_OnTouchPressed;
        character.Animator.CrossFade("Attack_Left_Forward_RH", 0.1f);
        playerAttacked = true;

        Debug.Log("You've entered attack state");
    }

    private void InputReader_OnTouchPressed()
    {

    }

    public override void Tick()
    {
        //character.transform.position += character.transform.forward * match.HorseSpeed * Time.deltaTime;
    }

    public override void FixedTick()
    {

    }

    public override void StateEnd()
    {
        Game.InputReader.OnTouchPressed -= InputReader_OnTouchPressed;
    }
}