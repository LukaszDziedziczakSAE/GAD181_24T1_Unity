using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupply_FiringLine : MonoBehaviour
{
    public List<Character> enemiesInLine = new List<Character>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Character enemy = other.GetComponent<Character>();
            if (enemy != null && !enemiesInLine.Contains(enemy))
            {
                enemiesInLine.Add(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Character enemy = other.GetComponent<Character>();
            if (enemy != null)
            {
                enemiesInLine.Remove(enemy);
            }
        }
    }
}  


