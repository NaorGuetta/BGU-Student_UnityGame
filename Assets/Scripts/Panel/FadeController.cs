using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class FadeController : MonoBehaviour
{
    private enum OverlayState
    {
        Hidden,
        FadingHiddenToBlack,
        Black,
        FadingBlackToVisible,
        Visible,
        FadingVisibleToHidden
    }

    public Image fadePanel;
    public Image blackSlate;
    public TextMeshProUGUI helloText;
    public TextMeshProUGUI daysInUniText;
    public TextMeshProUGUI daysSurvived;
    public float fadeDuration = 0.5f;
    public float overlayTimeout = 2.0f;
    public float dayCounterRewindDuration = 1.0f;
    private static float rewindStartDays;

    private OverlayState overlayState = OverlayState.Hidden;
    private float fadeStartTime;
    private float rewindStartTime;
    private bool rewindingDaysCounter;
    private List<string> names = new List<string> { "תומר", "איציק", "נאור", "משה","חיים", "מיכאל", "דן","גיא", "נדב", "יוני", "רונן", "תאמר", "עומר", "אופיר" };
    private void Start()
    {
        /*SetOverlayVisible(false);
        SetBlackSlateVisible(true);
        overlayState = OverlayState.Black;
        UpdateHelloTextWithRandomName();
        FadeIn();*/
        SetOverlayVisible(false);
        SetBlackSlateVisible(true);
        overlayState = OverlayState.Black;
    }

    public void StartFadeAnimation() {
        
        UpdateHelloTextWithRandomName();
        FadeIn();
        rewindStartDays = float.Parse(daysSurvived.text);

    }

    private void Update()
    {
        float fadeProgress;
        switch (overlayState)
        {
            case OverlayState.FadingHiddenToBlack:
                fadeProgress = (Time.time - fadeStartTime) / fadeDuration;
                if (fadeProgress > 1.0f)
                {
                    SetBlackSlateAlpha(1.0f);
                    overlayState = OverlayState.Black;
                    FadeToVisible();
                }
                else
                {
                    SetBlackSlateAlpha(Mathf.Clamp01(fadeProgress));
                }
                break;

            case OverlayState.FadingBlackToVisible:
                fadeProgress = (Time.time - fadeStartTime) / fadeDuration;
                if (fadeProgress > 1.0f)
                {
                    SetBlackSlateVisible(false);
                    overlayState = OverlayState.Visible;
                    DelayForSeconds(FadeOut, overlayTimeout);
                    rewindStartTime = Time.time;
                    rewindingDaysCounter = true;
                }
                else
                {
                    SetBlackSlateAlpha(Mathf.Clamp01(1.0f - fadeProgress));
                }
                break;

            case OverlayState.Visible:
                if (rewindingDaysCounter)
                {
                    float rewindProgress = (Time.time - rewindStartTime) / dayCounterRewindDuration;
                    if (rewindProgress > 1.0f)
                    {
                        rewindingDaysCounter = false;
                        SetDaysSurvived(0);
                    }
                    else
                    {

                        SetDaysSurvived((int)Mathf.Lerp(rewindStartDays, 0.0f, rewindProgress));
                    }
                }
                break;

            case OverlayState.FadingVisibleToHidden:
                fadeProgress = (Time.time - fadeStartTime) / fadeDuration;
                if (fadeProgress > 1.0f)
                {
                    SetOverlayVisible(false);
                    overlayState = OverlayState.Hidden;
                }
                else
                {
                    SetOverlayAlpha(Mathf.Clamp01(1.0f - fadeProgress));
                }
                break;
        }
    }

    private void FadeIn()
    {
        switch (overlayState)
        {
            case OverlayState.Hidden:
                FadeToBlack();
                break;
            case OverlayState.Black:
                FadeToVisible();
                break;
            case OverlayState.FadingVisibleToHidden:
                FadeToBlack();
                break;
        }
    }

    private void FadeToBlack()
    {
        fadeStartTime = Time.time;
        SetBlackSlateEnabled(true);
        overlayState = OverlayState.FadingHiddenToBlack;
    }

    private void FadeToVisible()
    {
        fadeStartTime = Time.time;
        SetOverlayVisible(true);
        overlayState = OverlayState.FadingBlackToVisible;
    }

    private void FadeOut()
    {
        fadeStartTime = Time.time;
        overlayState = OverlayState.FadingVisibleToHidden;
        FindObjectOfType<GameManager>().StartGameSeq();
    }


    private void SetOverlayEnabled(bool enabled)
    {
        fadePanel.enabled = enabled;
        helloText.enabled = enabled;
        daysInUniText.enabled = enabled;
        daysSurvived.enabled = enabled;
    }

    private void SetOverlayAlpha(float alpha)
    {
        SetColorAlpha(fadePanel, alpha);
        SetColorAlpha(helloText, alpha);
        SetColorAlpha(daysInUniText, alpha);
        SetColorAlpha(daysSurvived, alpha);
    }

    private void SetOverlayVisible(bool visible)
    {
        SetOverlayEnabled(visible);
        SetOverlayAlpha(visible ? 1.0f : 0.0f);
    }

    private static void SetColorAlpha(Graphic image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    private void DelayForSeconds(System.Action callback, float seconds)
    {
        StartCoroutine(DelayCoroutine(callback, seconds));
    }

    private IEnumerator DelayCoroutine(System.Action callback, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }

    private void SetBlackSlateEnabled(bool enabled)
    {
        blackSlate.enabled = enabled;
    }

    private void SetBlackSlateAlpha(float alpha)
    {
        SetColorAlpha(blackSlate, alpha);
    }

    private void SetBlackSlateVisible(bool visible)
    {
        SetBlackSlateEnabled(visible);
        SetBlackSlateAlpha(visible ? 1.0f : 0.0f);
    }

    public void SetDaysSurvived(int days)
    {
        daysSurvived.text = days.ToString();
    }

    private void UpdateHelloTextWithRandomName()
    {
        // Select a random name from the list
        string randomName = names[Random.Range(0, names.Count)];
        // Update the helloText with the new message
        helloText.text = $"ברכותי {randomName},\n התקבלת לאוניברסיטת בן גוריון בנגב !";
    }
}
