using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class CharacterBrain : MonoBehaviour
{

    [SerializeField] private Collider2D _collider;
    [SerializeField] private Rigidbody2D _rb2D;
    [SerializeField] private AnimationController _animControl;

    [SerializeField] private bool _isGrounded;
    private bool _hasBeenDamaged = false;

    private int _lives = 3;

    public event Action OnNewEggCame;
    public event Action OnCharacterReady;
    public event Action OnCharacterHurt;
    public event Action OnCharacterDie;
    public event Action OnCharacterFinishedDying;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Obstacles")
        {
            if (_lives > 0 && !_hasBeenDamaged)
            {
                _lives--;
                StartCoroutine(IncibilityTime());

                if (_lives > 0)
                {
                    SoundManager.Instance.PlayFX(SoundManager.Instance.sfxClips[(int)SfxClipType.Hurt]);
                    _animControl.GetHurt();
                    OnCharacterHurt?.Invoke();
                }
                else
                {
                    SoundManager.Instance.PlayFX(SoundManager.Instance.sfxClips[(int)SfxClipType.Die]);
                    OnCharacterDie?.Invoke();
                    _animControl.Die();
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            _isGrounded = true;
            _rb2D.gravityScale = 2;
        }
    }


    void Awake()
    {
        _animControl.OnHatch += OnHatch;
        _animControl.OnEggReady += OnNewEgg;
        _animControl.OnFinishedDying += OnFinishedDying;
    }

    public void OnHatch()
    {
        OnCharacterReady?.Invoke();
    }
    public void OnNewEgg()
    {
        OnNewEggCame?.Invoke();
        _hasBeenDamaged = false;
        _lives = 3;
    }
    public void OnFinishedDying()
    {
        OnCharacterFinishedDying?.Invoke();
    }
    public void NewEggSpawn()
    {
        _animControl.SpawnEgg();
    }
    public void Jump()
    {
        if (_isGrounded)
        {
            SoundManager.Instance.PlayFX(SoundManager.Instance.sfxClips[(int)SfxClipType.Jump]);
            _animControl.Jump();
            _rb2D.AddForce(new Vector2(_rb2D.velocity.x, 500));
            _isGrounded = false;
            StartCoroutine(LandCheck());
        }
        else
        {
            _rb2D.gravityScale *= 10;
        }
    }

    public IEnumerator LandCheck()
    {
        while (!_isGrounded)
        {
            yield return new WaitForSeconds(0.01f);
        }
        _animControl.JumpIsDone();
        yield return null;
    }
    public IEnumerator IncibilityTime()
    {
        _hasBeenDamaged = true;
        yield return new WaitForSeconds(1f);
        _hasBeenDamaged = false;
    }


    private void Die()
    {

    }
}
