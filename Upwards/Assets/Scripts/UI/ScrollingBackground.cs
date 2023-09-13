using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{

    [SerializeField]
    private float _speed = 2f;
    private Vector3 _startPos;

    [SerializeField]
    private float _waitTime = 2f;
    private bool _isMoving = false;

    private float _timer = 0.0f;
    private float _songTime;
    private AudioManager _audManager;

    [SerializeField]
    private GameObject _fadeObj;
    private Image _fadeImage;

    [SerializeField]
    private GameObject _tower;

    private bool _fadeBool = false;

    private void Start()
    {
        _audManager = FindObjectOfType<AudioManager>();

        // Não to conseguindo dar um drag do objeto de fade dentro desse lugar por algum motivo
        _fadeObj = GameObject.Find("Fade");
        _fadeImage = _fadeObj.GetComponent<Image>();
        _fadeImage.CrossFadeAlpha(0, _audManager.GetSource("Intro").clip.length, false);

        _audManager.Play("Intro");
        _songTime = _audManager.GetSource("Tema").clip.length;

        _timer = _waitTime;

        _startPos = _tower.transform.position;
        Invoke("StartTheme", _audManager.GetSource("Intro").clip.length);
    }

    private void Update()
    {
        if (_isMoving)
        {
            
            _timer += Time.deltaTime;
            TowerMovement();
            

            if(_songTime - _timer <= 9.0f)
            {
                FadeOut();

                if (_timer >= _songTime)
                {
                    Restart();
                }
            }
        }
    }

    private void StartTheme()
    {
        _audManager.Play("Tema");
        _songTime = _audManager.GetSource("Tema").clip.length;
        
        Invoke("StartMoving", _waitTime);
    }

    private void StartMoving()
    {
        _isMoving = true;
    }

    private void Restart()
    {
        
        _fadeImage.CrossFadeAlpha(0, 3f, false);

        _tower.transform.position = _startPos;
        _isMoving = false;
        _timer = _waitTime;
        _fadeBool = false;
        Invoke("StartMoving", _waitTime);
    }

    private void TowerMovement()
    {
        _tower.transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
    
    private void FadeOut()
    {
        if (_fadeBool == true) 
            return;

        _fadeBool = true;
        _fadeImage.CrossFadeAlpha(1, 9f, false);
    }
}