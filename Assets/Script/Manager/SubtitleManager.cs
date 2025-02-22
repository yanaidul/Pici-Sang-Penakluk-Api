using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] private SubtitleData subtitleDataLevel1;
    [SerializeField] private SubtitleData subtitleDataLevel2;
    [SerializeField] private SubtitleData subtitleDataLevel3;
    [SerializeField] private TextMeshProUGUI subtitleText;

    private Coroutine _subtitleCoroutine;

    public void PlayLevel1Subtitle()
    {
        ResetCoroutineIfCoroutineExist();
       _subtitleCoroutine = StartCoroutine(PlaySubtitles(subtitleDataLevel1));
    }

    public void PlayLevel2Subtitle()
    {
        ResetCoroutineIfCoroutineExist();
        _subtitleCoroutine = StartCoroutine(PlaySubtitles(subtitleDataLevel2));
    }

    public void PlayLevel3Subtitle()
    {
        ResetCoroutineIfCoroutineExist();
        _subtitleCoroutine = StartCoroutine(PlaySubtitles(subtitleDataLevel3));
    }

    private IEnumerator PlaySubtitles(SubtitleData selectedSubtitleData)
    {
        foreach (var line in selectedSubtitleData.lines)
        {
            yield return new WaitForSeconds(line.startTime);
            subtitleText.SetText(line.text);

            yield return new WaitForSeconds(line.duration);
            subtitleText.text = "";
        }
    }

    private void ResetCoroutineIfCoroutineExist()
    {
        if (_subtitleCoroutine != null)
        {
            StopCoroutine(_subtitleCoroutine);
            _subtitleCoroutine = null;
        }
    }
}