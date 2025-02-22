using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelFinish : PanelSimple
{
    [SerializeField] private GameObject _continueButton;
    [Space]
    [SerializeField] AudioTrigger audioTrigger;
    [SerializeField] SubtitleManager subtitleManager;

    [Space]
    [SerializeField] string strNextLevel = "Gameplay - ";

    [Space]
    [SerializeField] bool boolSkipToNextLevel = true;

    private Coroutine delayedMulaiButtonCoroutine;

    public override void Open()
    {
        if (delayedMulaiButtonCoroutine != null)
        {
            StopCoroutine(delayedMulaiButtonCoroutine);
            delayedMulaiButtonCoroutine = null;
        }

        LevelManager.Instance.Unlock();
        if(subtitleManager != null)
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 1:
                    _continueButton.gameObject.SetActive(false);
                    subtitleManager.PlayLevel2Subtitle();
                    delayedMulaiButtonCoroutine = StartCoroutine(OnDelayContinueButtonAppear(19));
                    break;
                case 2:
                    _continueButton.gameObject.SetActive(false);
                    subtitleManager.PlayLevel3Subtitle();
                    delayedMulaiButtonCoroutine = StartCoroutine(OnDelayContinueButtonAppear(20));
                    break;
            }
        }

        if (boolSkipToNextLevel)
            BtnNextLevel();
        else
            base.Open();
    }

    public void BtnVoice()
    {
        audioTrigger.PlayAudio();
    }

    public void BtnMainMenu()
    {
        PlayerPrefs.SetInt("Trigger Panel Level", 0);
        SceneManager.LoadScene("Main Menu");
    }

    public void BtnNextLevel()
    {
        SceneManager.LoadScene(strNextLevel);
    }

    public void BtnLevelMenu()
    {
        PlayerPrefs.SetInt("Trigger Panel Level", 1);
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator OnDelayContinueButtonAppear(float duration)
    {
        yield return new WaitForSeconds(duration);
        _continueButton.gameObject.SetActive(true);
    }
}
