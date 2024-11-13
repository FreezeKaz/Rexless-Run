using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private float _speed;
    private float _step;
    public event Action<GameObject> ObjectHasToDisappear;
    // Start is called before the first frame update
    public void Init()
    {
        _speed = GameManager.Instance._speed;
        _step = GameManager.Instance._step;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "DisableObject")
        {
            gameObject.SetActive(false);
            ObjectHasToDisappear?.Invoke(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance._playing)
        {
            _speed = GameManager.Instance._speed;
            transform.position = new Vector3(transform.position.x - _speed * _step * Time.deltaTime, transform.position.y, transform.position.z);

        }
    }
}
