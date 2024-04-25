using UnityEngine;

public class CS_ArrowSupply_ArcherFiring : CharacterState
{
    ArrowSupply_ArcherSupply.ArrowRecord arrowRecord;
    ArrowSupply_ArrowFirePoint firePoint;
    private ArrowSupply_Match match => (ArrowSupply_Match)Game.Match;

    private float delayBeforeFiring = 1.0f;
    private float timer = 0.0f;
    private bool hasFired = false;

    private Character lastTargetedEnemy; // Declare the variable to store the target

    // Modify the constructor to accept the last targeted enemy
    public CS_ArrowSupply_ArcherFiring(Character character, ArrowSupply_ArcherSupply.ArrowRecord arrowRecord, Character lastTargetedEnemy) : base(character)
    {
        this.arrowRecord = arrowRecord;
        this.lastTargetedEnemy = lastTargetedEnemy; // Store the passed target
        character.Animator.CrossFade("TargetShooting_BowIdle", 0.1f);
    }

    public override void StateStart()
    {
        firePoint = character.GetComponentInChildren<ArrowSupply_ArrowFirePoint>();
        timer = 0.0f;
        hasFired = false;
    }

    public override void Tick()
    {
        if (!hasFired)
        {
            timer += Time.deltaTime;

            if (timer >= delayBeforeFiring)
            {
                // Use lastTargetedEnemy for firing
                if (firePoint != null && lastTargetedEnemy != null)
                {
                    firePoint.FireArrow(arrowRecord, lastTargetedEnemy);
                    hasFired = true;

                    character.SetNewState(new CS_ArrowSupply_ArcherWaiting(character)); // Switch to waiting state after firing
                }
            }
        }
    }

    public override void StateEnd()
    {
    }

    public override void FixedTick()
    {
    }

}
