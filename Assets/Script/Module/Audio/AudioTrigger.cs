using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;

    [Space]
    [SerializeField] bool boolOnEnable;
    [SerializeField] float fltDelayOnEnable = .2f;
    [SerializeField] float fltBgmVolOnPlay = -1;

    private void Awake()
    {
        if (boolOnEnable)
            return;

        TryGetComponent(out Button btn);

        if(btn)
            btn.onClick.AddListener(PlayAudio);
    }

    public void PlayAudio()
    {
        if(fltBgmVolOnPlay < 0)
            AudioManager.Instance.SetSFX(audioClip);
        else
            AudioManager.Instance.SetSFX(audioClip, fltBgmVolOnPlay, audioClip.length);
    }

    public void PlayAudio(AudioClip _audioClip)
    {
        audioClip = _audioClip;

        PlayAudio();
    }

    private IEnumerator CoroutineDelay()
    {
        yield return new WaitForSeconds(fltDelayOnEnable);

        PlayAudio();
    }

    private void OnEnable()
    {
        if(boolOnEnable)
            StartCoroutine(CoroutineDelay());
    }
}
