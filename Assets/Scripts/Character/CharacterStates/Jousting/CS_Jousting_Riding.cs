using UnityEngine;

public class CS_Jousting_Riding : CharacterState
{
    private JoustingMatch match;
    private Character other;
    public Jousting_AI ai;

    private bool isAttacking = false;

    public CS_Jousting_Riding(Character character) : base(character)
    {
        match = (JoustingMatch)Game.Match;
        other = match.OtherCharacter(character);
    }

    public override void StateStart()
    {
        ai = GameObject.FindObjectOfType<Jousting_AI>(); // Use FindObjectOfType to get a single instance
        character.Animator.CrossFade("Jousting_Rider_Gallop", 0.1f);

        if (character.HorseAnimator != null)
        {
            character.HorseAnimator.CrossFade("LocomotionBlend", 0.1f);
            character.HorseAnimator.SetFloat("speed", 1);
        }

        // Activate a random trigger
        ActivateRandomTrigger();

        if (IsPlayerCharacter) Game.InputReader.OnTouchPressed += InputReader_OnTouchPressed;
    }

    private void ActivateRandomTrigger()
    {
        GameObject[] triggers = GameObject.FindGameObjectsWithTag("AttackTrigger");
        if (triggers.Length > 0)
        {
            int randomIndex = Random.Range(0, triggers.Length);
            triggers[randomIndex].SetActive(true);
        }
    }


    public override void Tick()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movementDirection = new Vector3(0f, 0f, match.HorseSpeed); //redo

        if (character.PlayerIndex == 0)
        {
            character.transform.position += movementDirection.normalized * match.HorseSpeed * Time.deltaTime;
            //ui.JoustingIndicator.UpdateDistanceIndicator(Distance());
            //ui.JoustingIndicator.UpdateStrikingDistanceIndicator(IsWithinJoustingDistance());
            //ui.EndIndicator.UpdateEndIndicator(ReachedEnd());
            match.PlayerReachedEnd(character);
        }
        else if (character.PlayerIndex == 1)
        {
            character.transform.position -= movementDirection.normalized * match.HorseSpeed * Time.deltaTime;
            //ui.EnemyJoustingIndicator.UpdateDistanceIndicator(Distance());
            //ui.EnemyJoustingIndicator.UpdateStrikingDistanceIndicator(IsWithinJoustingDistance());
        }
    }

    //public void TriggerAttackIfInRange()
    //{
    //    if (ai != null && IsWithinJoustingDistance())
    //    {
    //        ai.Attack();
    //    }
    //}


    public override void FixedTick()
    {

    }

    public override void StateEnd()
    {
        if (IsPlayerCharacter) Game.InputReader.OnTouchPressed -= InputReader_OnTouchPressed;
    }

    //private float Distance()
    //{
    //    //float distance = 0;

    //    //if (character.PlayerIndex == 0)
    //    //{
    //    //    distance = other.transform.position.z - character.transform.position.z;
    //    //    Debug.Log("player position: " + character.transform.position.z);
    //    //}
    //    //else if (character.PlayerIndex == 1)
    //    //{
    //    //    distance = character.transform.position.z - other.transform.position.z;
    //    //    Debug.Log("ai position: " + other.transform.position.z);
    //    //}

    //    //Debug.Log("Distance: " + distance); 

    //    //if (distance < 0)
    //    //{
    //    //    distance = 0;
    //    //}

    //    return distance;
    //}

    //public bool IsWithinJoustingDistance()
    //{
    //    //float distance = Distance();
    //    //bool withinDistance = distance >= match.MinimumJoustingDistance && distance <= match.MaximumJoustingDistance;
    //    //Debug.Log("Distance: " + distance + ", Within Jousting Distance: " + withinDistance);
    //    //return withinDistance;
    //}


    private float PlayerPosition()
    {
        float position = 0;

        if (character.PlayerIndex == 0)
        {
            position = character.transform.position.z;
        }

        return position;
    }

    public bool ReachedEnd()
    {
        float position = PlayerPosition();
        return position >= match.EndDistance;
    }

    private void InputReader_OnTouchPressed()
    {
        character.SetNewState(new CS_Jousting_Attack(character));
    }
}