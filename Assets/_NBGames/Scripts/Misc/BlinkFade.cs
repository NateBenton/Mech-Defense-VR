using System;
using System.Collections;
using _NBGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _NBGames.Scripts.Misc
{
    public class BlinkFade : MonoBehaviour
    {
        private Canvas _canvas;
        [SerializeField] private Image _blinkPanel;
        [SerializeField] private float _blinkTime;
        [SerializeField] private float _fadeSpeed = 4f;

        private bool _fadeIn, _fadeOut;
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();

            if (_canvas == null)
            {
                Debug.LogError("Canvas null on " + gameObject.name);
            }
        }

        private void OnEnable()
        {
            EventManager.onAllCurrentEnemiesKilled += EnableFadeout;
        }

        private void OnDisable()
        {
            EventManager.onAllCurrentEnemiesKilled -= EnableFadeout;
        }

        private void Update()
        {
            if (_fadeOut)
            {
                Fadeout();
            }
            else if (_fadeIn)
            {
                Fadein();
            }
        }

        private void EnableFadeout()
        {
            _canvas.enabled = true;
            _fadeOut = true;
        }

        private void Fadein()
        {
            var fadeColor = _blinkPanel.color;
            fadeColor.a = Mathf.MoveTowards(fadeColor.a, 0f, _fadeSpeed * Time.deltaTime);
            _blinkPanel.color = fadeColor;

            if (!(fadeColor.a <= 0f)) return;
            _fadeIn = false;
            _canvas.enabled = false;
        }

        private void Fadeout()
        {
            var fadeColor = _blinkPanel.color;
            fadeColor.a = Mathf.MoveTowards(fadeColor.a, 1f, _fadeSpeed * Time.deltaTime);
            _blinkPanel.color = fadeColor;

            if (!(fadeColor.a >= 1f)) return;
            EventManager.BlinkEnabled();
            _fadeOut = false;
            StartCoroutine(BlinkWait());

        }

        private IEnumerator BlinkWait()
        {
            yield return new WaitForSeconds(_blinkTime);
            _fadeIn = true;
        }
    }
}
