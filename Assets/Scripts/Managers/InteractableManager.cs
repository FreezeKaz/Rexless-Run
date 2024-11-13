using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableManager : MonoBehaviour
{
    [SerializeField] private OnClickedJump _jumpInteractable;
    [SerializeField] private CharacterBrain _brain;
    private void Awake()
    {
        _jumpInteractable.OnJump += CallJump;
    }

    private void CallJump()
    {
        _brain.Jump();
    }

}
