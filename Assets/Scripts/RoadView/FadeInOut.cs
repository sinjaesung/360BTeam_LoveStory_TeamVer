using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _fadeTime=2f;
    private void Start()
    {
        _image = GetComponent<Image>();
    }
    public void StartFadeIn()
    {
        StartCoroutine(Fade(1, 0));
    }

    public void StartFadeOut()
    {
        StartCoroutine(Fade(0, 1));
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / _fadeTime;

            Color color = _image.color;
            //color.a = Mathf.Lerp(start, end, 1-percent);
            var alpha = start + (end - start) * percent;//0+1*percent 진해지는, 1+ -1*percent 흐려지는
            color.a = alpha;
            //_image.fillAmount = Mathf.Lerp(start, end, 1 - percent);
            _image.fillAmount = alpha;

            _image.color = color;

            yield return null;
        }
    }
    public float GetFadeTime()
    {
        return _fadeTime;
    }
}
