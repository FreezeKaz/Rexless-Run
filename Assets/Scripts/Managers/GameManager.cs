using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] private UIManager _uiManager;
    [SerializeField] private CharacterBrain _characterBrain;
    [SerializeField] private InteractableManager _interactableManager;
    [SerializeField] private ItemSpawner _itemSpawner;

    [SerializeField] private List<Scroller> _scrollers = new List<Scroller>();

    [SerializeField] public float _speed;
    [SerializeField] public float _step;
    [SerializeField] public float _cloudStep;
    [NonSerialized] public bool _playing = false;
    [NonSerialized] public int Difficulty = 1;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        _characterBrain.OnNewEggCame += _uiManager.EndGameUIDeactivate;
        _characterBrain.OnCharacterReady += StartGame;
        _characterBrain.OnCharacterHurt += OnCharacterHit;
        _characterBrain.OnCharacterDie += StopGame;
        _characterBrain.OnCharacterFinishedDying += _uiManager.EndGameUIActivate;

        _uiManager.OnScreenDark += EnableNewGameAnim;

        SoundManager.Instance.PlayMusic(SoundManager.Instance.musicClips[(int)Musics.Menu]);
    }

    public void StartGame()
    {
      
        SoundManager.Instance.PlayMusic(SoundManager.Instance.musicClips[(int)Musics.Game]);
        _playing = true;
        _itemSpawner.StartSpawning();
        _interactableManager.gameObject.SetActive(true);
        foreach (var scroller in _scrollers) { scroller.StartScroll(); }
    }

    public void StopGame()
    {
        _playing = false;
        _itemSpawner.StopSpawning();
        _interactableManager.gameObject.SetActive(false);
        foreach (var scroller in _scrollers) { scroller.StopScroll(); }
       
    }

    public void EnableNewGameAnim()
    {
        _itemSpawner.DisableObstacles();
        _characterBrain.NewEggSpawn();
        _speed = 1;

    }
    public void OnCharacterHit()
    {
        StartCoroutine(FreezeTime());
    }
    public IEnumerator FreezeTime()
    {
        Time.timeScale = 0.5f; // Small slow-motion effect
        yield return new WaitForSecondsRealtime(0.03f);
        Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        if (_playing)
            _speed += 0.001f;
    }

    private void OnDestroy()
    {
        _characterBrain.OnNewEggCame -= _uiManager.EndGameUIDeactivate;
        _characterBrain.OnCharacterReady -= StartGame;
        _characterBrain.OnCharacterHurt -= OnCharacterHit;
        _characterBrain.OnCharacterDie -= StopGame;
        _characterBrain.OnCharacterFinishedDying -= _uiManager.EndGameUIActivate;

        _uiManager.OnScreenDark -= EnableNewGameAnim;
    }
}
