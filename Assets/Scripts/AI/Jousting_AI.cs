using UnityEngine;

public class Jousting_AI : AI
{
    private CS_Jousting_Riding riding;
    private CS_Jousting_Impact impact;
    private Jousting_Weapon weapon;

    public void Attack(Vector3 attackPosition)
    {
        //Debug.Log("Attack method called at position: " + attackPosition);

        character.Animator.CrossFade("Jousting_Rider_Attack", 0.1f);
        weapon.SetOwner(character);
    }
}
