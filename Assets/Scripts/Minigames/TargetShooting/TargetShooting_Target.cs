using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooting_Target : MonoBehaviour
{
    [SerializeField] float upRotation;
    [SerializeField] float downRotation;
    [SerializeField] float rotationTime = 0.5f;
    float rotStartTime;
    Collider target;
    float rotProgress => (Time.time - rotStartTime) / rotationTime;

    [field: SerializeField, Header("DEBUG")] public EState State {  get; private set; } 

    public enum EState
    {
        none,
        upPoisition,
        movingUp,
        downPosition,
        movingDown
    }

    private void Update()
    {
        if (State == EState.movingUp)
        {
            float rotation = Mathf.LerpAngle(downRotation, upRotation, rotProgress);

            if (rotProgress < 1) transform.eulerAngles = new Vector3(rotation, transform.eulerAngles.y, transform.eulerAngles.z);

            else
            {
                transform.eulerAngles = new Vector3(upRotation, transform.eulerAngles.y, transform.eulerAngles.z);
                State = EState.upPoisition;
            }
        }

        else if (State == EState.movingDown)
        {
            float rotation = Mathf.LerpAngle(upRotation, downRotation, rotProgress);
            transform.eulerAngles = new Vector3(rotation, transform.eulerAngles.y, transform.eulerAngles.z);

            if (rotProgress < 1) transform.eulerAngles = new Vector3(rotation, transform.eulerAngles.y, transform.eulerAngles.z);

            else
            {
                transform.eulerAngles = new Vector3(downRotation, transform.eulerAngles.y, transform.eulerAngles.z);
                State = EState.downPosition;
            }
        }
    }


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
        target = GetComponent<Collider>();
        target.enabled = true;
    }

    public void SetDownRoation()
    {
        Debug.Log(name + " setting Down");
        transform.eulerAngles = new Vector3(downRotation, transform.eulerAngles.y, transform.eulerAngles.z);
        target = GetComponent<Collider>();
        target.enabled = false;
    }

    public void StartRotatingDown()
    {
        rotStartTime = Time.time;
        State = EState.movingDown;
    }

    public void StartRotatingUp()
    {
        rotStartTime = Time.time;
        State = EState.movingUp;
    }
}
