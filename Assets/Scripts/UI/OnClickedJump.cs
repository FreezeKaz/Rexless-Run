using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickedJump : MonoBehaviour
{
    private bool canProcessTouch = true;
    public event Action OnJump;


    void Update()
    {
        // Check if there's any touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (IsPointerOverUI(touch))
            {
                canProcessTouch = false;
            }
            else
            {
                canProcessTouch = true;
            }
            if (touch.phase == TouchPhase.Began)
            {
                OnTouch();
            }
        }
    }

   
    void OnTouch()
    {
        if (canProcessTouch)
        {
            OnJump?.Invoke();
            Debug.Log("Haha");
        }
    }
    bool IsPointerOverUI(Touch touch)
    {
        return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
    }
}
