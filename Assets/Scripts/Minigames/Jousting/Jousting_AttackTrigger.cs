using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered trigger");
        
        Jousting_AI ai = other.GetComponent<Jousting_AI>();
        if (ai != null)
        {
            ai.Attack();
        }
    }
}