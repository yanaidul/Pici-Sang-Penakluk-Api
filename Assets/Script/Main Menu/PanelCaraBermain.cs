using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelCaraBermain : PanelSimple
{
    [SerializeField] private GameObject _mulaiButton;
    private string strScene;
    private Coroutine delayedMulaiButtonCoroutine;

    private AudioClip sfxCaraBermain;

    [Header("Reference")]
    private AudioTrigger audioTrigger;

    private void Awake()
    {
        audioTrigger = GetComponent<AudioTrigger>();
    }

    public override void Open()
    {
        base.Open();
        _mulaiButton.gameObject.SetActive(false);
        audioTrigger.PlayAudio(sfxCaraBermain);
    }

    public void Setup(string _strScene, AudioClip _sfxCaraBermain)
    {
        Debug.Log("Setup cara bermain");
        if (delayedMulaiButtonCoroutine != null)
        {
            StopCoroutine(delayedMulaiButtonCoroutine);
            delayedMulaiButtonCoroutine = null;
            Debug.Log("Stop coroutine");    
        }

        strScene = _strScene;
        sfxCaraBermain = _sfxCaraBermain;
        switch(_strScene)
        {
            case "Gameplay - City":
                delayedMulaiButtonCoroutine = StartCoroutine(OnDelayMulaiButtonAppear(11));
                break;
            case "Gameplay - House 1":
                delayedMulaiButtonCoroutine = StartCoroutine(OnDelayMulaiButtonAppear(19));
                break;
            case "Gameplay - School 1":
                delayedMulaiButtonCoroutine = StartCoroutine(OnDelayMulaiButtonAppear(20));
                break;
        }

        Open();
    }

    public void BtnMulai()
    {
        SceneManager.LoadScene(strScene);
    }

    IEnumerator OnDelayMulaiButtonAppear(float duration)
    {
        Debug.Log("delay mulai button");
        yield return new WaitForSeconds(duration);
        _mulaiButton.gameObject.SetActive(true);
    }
}
