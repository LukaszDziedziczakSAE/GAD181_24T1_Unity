using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooting_Target : MonoBehaviour
{
    [SerializeField] float upRotation;
    [SerializeField] float downRotation;

    public bool IsDown => transform.eulerAngles.x == downRotation;

    public bool IsUp => transform.eulerAngles.x == upRotation;

    public void ActivateTarget()
    {
        SetUpRotation();
    }

    public void SetUpRotation()
    {
        Debug.Log(name + " setting Up");
        transform.eulerAngles = new Vector3(upRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void SetDownRoation()
    {
        Debug.Log(name + " setting Down");
        transform.eulerAngles = new Vector3(downRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
