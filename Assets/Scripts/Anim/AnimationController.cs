using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    [SerializeField] private Animator _animator;

    public event Action OnHatch;
    public event Action OnFinishedDying;
    public event Action OnEggReady;
    // Start is called before the first frame update
    public void HatchEgg()
    {
        _animator.SetBool("Hatch", true);
    }


    // Update is called once per frame
    public void StartWalking()
    {
        OnHatch?.Invoke();
        _animator.SetBool("Walk", true);
    }
    public void SpawnEgg()
    {
        _animator.SetBool("NewEgg", true);
    }
    public void EggHasArrived()
    {
        OnEggReady?.Invoke();
        _animator.SetBool("NewEgg", false);
        ResetAnim();
    }

    public void CharacterHasFinishedDying()
    {
        OnFinishedDying?.Invoke();
    }

    public void GetHurt()
    {
        _animator.SetBool("Hurt", true);
        _animator.SetBool("Fire", false);
        _animator.SetBool("Jump", false);
    }

    public void HurtIsDone()
    {
        _animator.SetBool("Hurt", false);

    }

    public void Jump()
    {
        _animator.SetBool("Jump", true);
    }

    public void JumpIsDone()
    {
        _animator.SetBool("Jump", false);
    }

    public void Fire()
    {
        _animator.SetBool("Fire", true);

    }

    public void FireIsDone()
    {
        _animator.SetBool("Fire", false);

    }

    public void Die()
    {
        _animator.SetBool("Die", true);
    }

    public void ResetAnim()
    {
        _animator.SetBool("Hatch", false);
        _animator.SetBool("Walk", false);
        _animator.SetBool("Hurt", false);
        _animator.SetBool("Fire", false);
        _animator.SetBool("Jump", false);
        _animator.SetBool("Die", false);
        _animator.SetBool("EggIdle", false);

    }









}
