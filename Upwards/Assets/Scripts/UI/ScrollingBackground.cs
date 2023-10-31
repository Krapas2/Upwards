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
    private Image _fadeImage;

    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private Animator _buttonAnim;
    [SerializeField]
    private Animator _fadeAnim;

    private bool _fadeBool = false;

    private void Start()
    {
        _audManager = FindObjectOfType<AudioManager>();

        _fadeImage.CrossFadeAlpha(0, _audManager.GetSource("Intro").clip.length, false);

        _audManager.Play("Intro");
        _songTime = _audManager.GetSource("Tema").clip.length;

        _timer = _waitTime;

        _startPos = _background.transform.position;
        Invoke("StartTheme", _audManager.GetSource("Intro").clip.length);
    }

    private void Update()
    {
        if (_isMoving)
        {

            _timer += Time.deltaTime;
            TowerMovement();


            if (_songTime - _timer <= 9.0f)
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
        _buttonAnim.SetTrigger("ButtonEntrance");
        _audManager.Play("Tema");
        _songTime = _audManager.GetSource("Tema").clip.length;

        Invoke("StartMoving", _waitTime);
    }

    private void StartMoving()
    {
        _isMoving = true;
        StartCoroutine(TurnCloudy());
    }

    private void Restart()
    {

        _fadeImage.CrossFadeAlpha(0, 3f, false);

        _background.transform.position = _startPos;
        _isMoving = false;
        _timer = _waitTime;
        _fadeBool = false;
        Invoke("StartMoving", _waitTime);
    }

    private void TowerMovement()
    {
        _background.transform.Translate(Vector3.down * (_speed / 100) * Time.deltaTime * Screen.width);
    }

    private void FadeOut()
    {
        if (_fadeBool == true)
            return;

        _fadeBool = true;
        _fadeImage.CrossFadeAlpha(1, 9f, false);
    }

    private IEnumerator TurnCloudy()
    {
        yield return new WaitForSeconds(15f);

        _fadeImage.CrossFadeAlpha(1, 2f, false);
        _fadeAnim.SetTrigger("Cloudy");

        yield return new WaitForSeconds(12f);

        _fadeImage.CrossFadeAlpha(0, 0f, false);
    }
}