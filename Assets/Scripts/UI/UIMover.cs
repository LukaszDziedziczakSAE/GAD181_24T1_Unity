using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMover : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Vector2 offScreenPosition;
    [SerializeField] Vector2 onScreenPosition;
    [SerializeField] float duration = 0.1f;

    [field: SerializeField] public EStatus Status { get; private set; }

    public event Action MoveOnScreenComplete;
    public event Action MoveOffScreenComplete;

    float timer;
    float progress => timer/duration;

    private void Start()
    {
        if (rectTransform == null) Debug.LogError(name + " missing rectTransform referance");
    }

    public enum EStatus
    {
        onScreen,
        offScreen,
        movingToOnScreen,
        movingToOffScreen
    }

    private void Update()
    {
        switch (Status)
        {
            case EStatus.movingToOnScreen:
                timer += Time.deltaTime;
                if (timer < duration) MovingOnScreen();
                else SetOnScreenPosition();
                break;

            case EStatus.movingToOffScreen:
                timer += Time.deltaTime;
                if (timer < duration) MovingOffScreen();
                else SetOffScreenPosition();
                break;
        }

    }

    private void MovingOnScreen()
    {
        Vector2 position = new Vector2(Mathf.Lerp(offScreenPosition.x, onScreenPosition.x, progress), Mathf.Lerp(offScreenPosition.y, onScreenPosition.y, progress));
        rectTransform.anchoredPosition = position;
    }

    private void MovingOffScreen()
    {
        Vector2 position = new Vector2(Mathf.Lerp(onScreenPosition.x, offScreenPosition.x, progress), Mathf.Lerp(onScreenPosition.y, offScreenPosition.y, progress));
        rectTransform.anchoredPosition = position;
    }

    public void SetOnScreenPosition()
    {
        rectTransform.anchoredPosition = onScreenPosition;
        Status = EStatus.onScreen;

        MoveOnScreenComplete?.Invoke();
        MoveOnScreenComplete = null;
    }

    public void SetOffScreenPosition()
    {
        rectTransform.anchoredPosition = offScreenPosition;
        Status = EStatus.offScreen;

        MoveOffScreenComplete?.Invoke();
        MoveOffScreenComplete = null;
    }

    public void MoveToOffScreen()
    {
        if (Status != EStatus.onScreen) return;
        Status = EStatus.movingToOffScreen;
        timer = 0;
    }

    public void MoveToOnScreen()
    {
        if (Status != EStatus.offScreen) return;
        Status = EStatus.movingToOnScreen;
        timer = 0;
    }
}
