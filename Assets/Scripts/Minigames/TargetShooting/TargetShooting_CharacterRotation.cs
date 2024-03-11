using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooting_CharacterRotation : MonoBehaviour
{
    float startingYPostition;
    float startingXPostition;
    [SerializeField] Transform player;
    Quaternion resetPlayerRotation;

    void VerticalAiming()
    {
        startingYPostition = Game.InputReader.TouchPosition.y;
        float currentYPosition = Game.InputReader.TouchPosition.y;
        float aimDirectionYAxis = startingYPostition - currentYPosition;

    }

    void HorizontalAiming()
    {
        startingXPostition = Game.InputReader.TouchPosition.x;
        float currentXPosition = Game.InputReader.TouchPosition.x;
        float aimDirectionXAxis = startingXPostition - currentXPosition;

        player.rotation = Quaternion.Euler(0, aimDirectionXAxis, 0);

        
    }


    private void CharacterRotation()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(player.position);
        Vector3 aimDirection = Input.mousePosition;
        Vector3 heading = playerScreenPosition - aimDirection;

        float angle = Mathf.Atan2(heading.y, heading.x) * Mathf.Rad2Deg + 90;
        player.rotation = Quaternion.Euler(0, 0, angle);
    }
}


