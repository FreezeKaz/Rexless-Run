using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public enum TYPE { CLOUD, GROUND };

    [SerializeField] private TYPE _type;
    private Vector3 _startPos;
    bool _isDragging = false;
    private float _speed;
    private float _step;
    [SerializeField] private float _resetPointX;
    [SerializeField] private float _respawnPointX;

    // Start is called before the first frame update
    void Start()
    {
        _speed = GameManager.Instance._speed;
        _step = _type == TYPE.GROUND ? GameManager.Instance._step : GameManager.Instance._cloudStep;
        _startPos = transform.position;
    }
    public void StartScroll() { _isDragging = true; }
    public void StopScroll() 
    { 
        _isDragging = false; 
    }
    public void ResetScroller()
    {
        transform.position = _startPos;
    }
    // Update is called once per frame
    void Update()
    {
        _speed = GameManager.Instance._speed;
        if (_isDragging)
        {
            transform.position = new Vector3(transform.position.x - _speed * _step * Time.deltaTime, transform.position.y, transform.position.z);
            if(transform.position.x < _resetPointX)
            {
                transform.position = new Vector3(_respawnPointX, transform.position.y, transform.position.z);
            }
        }
    }
}
