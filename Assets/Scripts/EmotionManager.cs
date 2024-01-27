using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EmotionManager : MonoBehaviour
{
    public GameObject emotionBubble;
    public TextMeshProUGUI emoticonText;

    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;

    private CanvasGroup emotionCanvasGroup;

    private void Start()
    {
        emotionCanvasGroup = emotionBubble.GetComponent<CanvasGroup>();
    }

    public void ShowEmotion(string emotionText)
    {
        StopAllCoroutines();
        emoticonText.text = emotionText;
        StartCoroutine(FadeIn());
    }

    public void HideEmotion()
    {
        emotionBubble.SetActive(false);
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        emotionCanvasGroup.alpha = 0f;
        emotionBubble.SetActive(true);

        while (elapsedTime < fadeInDuration)
        {
            emotionCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        emotionCanvasGroup.alpha = 1f;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);

        float elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            emotionCanvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeOutDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        emotionCanvasGroup.alpha = 0f;
        emotionBubble.SetActive(false);
    }
}
