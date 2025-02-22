using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMainMenu : MonoBehaviour
{
    [Header("Reference")]
    private AudioManager audioManager;
    private PanelInfo panelInfo;
    private PanelLevel panelLevel;
    private PanelQuit panelQuit;

    private void Awake()
    {
        audioManager = AudioManager.Instance;
        panelInfo = GetComponent<PanelInfo>();
        panelLevel = GetComponent<PanelLevel>();
        panelQuit = GetComponent<PanelQuit>();

        if(PlayerPrefs.GetInt("Trigger Panel Level") == 1)
            BtnStart();

        PlayerPrefs.SetInt("Trigger Panel Level", 0);
    }

    public void BtnStart()
    {
        panelLevel.Open();
    }

    public void BtnInfo()
    {
        panelInfo.Open();
    }

    public void BtnAudio()
    {
        audioManager.UnMuteBGM();
        audioManager.UnMuteSFX();
    }

    public void BtnQuit()
    {
        panelQuit.Open();
    }
}
