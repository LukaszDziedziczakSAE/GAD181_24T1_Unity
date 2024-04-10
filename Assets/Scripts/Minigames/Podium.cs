using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Podium : MonoBehaviour
{
    [SerializeField] Transform position1;
    [SerializeField] Transform position2;
    [SerializeField] Transform position3;
    [SerializeField] Vector3 characterLocalRotation;
    [SerializeField] CinemachineVirtualCamera podiumCam;
    [SerializeField] CinemachineSmoothPath path;
    [SerializeField] float timeToShow; // in seconds
    [SerializeField] bool focusOnPlayer; // instead of winner

    CinemachineTrackedDolly trackedDolly;

    private float step => (path.m_Waypoints.Length-1) / timeToShow;

    private void Awake()
    {
        trackedDolly = podiumCam.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    public void Initilize(MatchResult matchResult)
    {
        Character first = null;
        Character second = null;
        Character third = null;
        Character forth = null;

        if (matchResult.Results.Length > 0) first = Game.CharacterByIndex(matchResult.Results[0].PlayerNumber);
        if (matchResult.Results.Length > 1) second = Game.CharacterByIndex(matchResult.Results[1].PlayerNumber);
        if (matchResult.Results.Length > 2) third = Game.CharacterByIndex(matchResult.Results[2].PlayerNumber);
        if (matchResult.Results.Length > 3) forth = Game.CharacterByIndex(matchResult.Results[3].PlayerNumber);

        if (first)
        {
            first.SetNewState(new CS_Podium(first, true));
            first.transform.parent = position1;
            first.transform.localPosition = Vector3.zero;
            first.transform.localEulerAngles = characterLocalRotation;
        }

        if (second != null)
        {
            second.SetNewState(new CS_Podium(second, false));
            second.transform.parent = position2;
            second.transform.localPosition = Vector3.zero;
            second.transform.localEulerAngles = characterLocalRotation;
        }

        if (third != null)
        {
            third.SetNewState(new CS_Podium(third, false));
            third.transform.parent = position3;
            third.transform.localPosition = Vector3.zero;
            third.transform.localEulerAngles = characterLocalRotation;
        }
        
        if (forth) Destroy(forth.gameObject);

        if (focusOnPlayer && Game.CharacterByIndex(0) != null)
        {
            podiumCam.LookAt = Game.CharacterByIndex(0).transform;
        }

        Game.CameraManager.SwitchTo(podiumCam);
    }

    private void Update()
    {
        if (Game.UI.MatchEnd.gameObject.activeSelf) return;

        trackedDolly.m_PathPosition += (step * Time.deltaTime);

        if (trackedDolly.m_PathPosition >= (path.m_Waypoints.Length - 1)) Game.Match.ShowPostMatchUI();
    }
}
