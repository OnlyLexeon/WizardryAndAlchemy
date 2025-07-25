﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Fades a canvas over time using a coroutine and a canvas group
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class FadeCanvas : MonoBehaviour
{
    [Tooltip("The speed at which the canvas fades")]
    public float defaultDuration = 1.0f;

    public Coroutine CurrentRoutine { private set; get; } = null;

    private CanvasGroup canvasGroup = null;
    private float alpha = 0.0f;

    private float quickFadeDuration = 0.25f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void StartFadeIn()
    {
        StopAllCoroutines();
        CurrentRoutine = StartCoroutine(FadeIn(defaultDuration));
    }

    public void StartFadeOut()
    {
        StopAllCoroutines();
        CurrentRoutine = StartCoroutine(FadeOut(defaultDuration));
    }

    public void QuickFadeIn()
    {
        StopAllCoroutines();
        CurrentRoutine = StartCoroutine(FadeIn(quickFadeDuration));
    }

    public void QuickFadeOut()
    {
        StopAllCoroutines();
        CurrentRoutine = StartCoroutine(FadeOut(quickFadeDuration));
    }

    private IEnumerator FadeIn(float duration)
    {
        float elapsedTime = 0.0f;

        alpha = 0;

        while (alpha <= 1.0f)
        {
            SetAlpha(elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOut(float duration)
    {
        float elapsedTime = 0.0f;

        alpha = 1;

        while (alpha >= 0.0f)
        {
            SetAlpha(1 - (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void StartFadeInOut()
    {
        StopAllCoroutines();
        CurrentRoutine = StartCoroutine(FadeInOut(defaultDuration));
    }

    private IEnumerator FadeInOut(float totalDuration)
    {
        float halfDuration = totalDuration / 2f;

        // Fade In
        float elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            SetAlpha(elapsedTime / halfDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetAlpha(1f);

        // Fade Out
        elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            SetAlpha(1f - (elapsedTime / halfDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetAlpha(0f);
    }

    private void SetAlpha(float value)
    {
        alpha = value;
        canvasGroup.alpha = alpha;
    }
}