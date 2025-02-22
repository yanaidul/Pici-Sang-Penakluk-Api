using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelPause : PanelSimple
{
    [Header("Reference")]
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = AudioManager.Instance;
    }

    public void BtnLevelMenu()
    {
        PlayerPrefs.SetInt("Trigger Panel Level", 1);
        SceneManager.LoadScene("Main Menu");
    }

    public void BtnResume()
    {
        Close();
    }

    public void BtnAudio()
    {
        audioManager.UnMuteBGM();
        audioManager.UnMuteSFX();
    }
}
