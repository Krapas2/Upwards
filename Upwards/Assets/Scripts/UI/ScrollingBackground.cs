using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    [SerializeField]
    private float _speed = 2f;
    private Vector3 _startPos;

    [SerializeField]
    private float _waitTime = 2f;
    private bool _isMoving = false;

    private float _timer = 0.0f;
    [SerializeField]
    private float _songTime = 69.0f;

    private void Start()
    {
        _startPos = transform.position;
        Invoke("StartMoving", _waitTime);
    }

    private void Update()
    {
        Debug.Log(_isMoving);

        if (_isMoving)
        {
            TowerMovement();
            _timer += Time.deltaTime;

            // Get audio clip's length somehow instead of raw number
            if (_timer >= _songTime)
            {
                Restart();
            }
        }
        else if (_timer == -1f) Invoke("StartMoving", _waitTime);

    }

    private void StartMoving()
    {
        _isMoving = true;
    }

    private void Restart()
    {
        transform.position = _startPos;
        _isMoving = false;
        _timer = -1.0f;
    }

    private void TowerMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
}