using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entered trigger: " + transform.position);

        Jousting_AI ai = other.GetComponent<Jousting_AI>();
        if (ai != null)
        {
            ai.Attack(transform.position);
        }
    }
}
