using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set; }

    [SerializeField] AudioSource audioBGM;
    [SerializeField] AudioSource audioSFX;

    private float fltInitBgmVolume;

    public bool BoolBGMMute => audioBGM.mute;
    public bool BoolSFXMute => audioSFX.mute;

    public float FltVolumeBGM => audioBGM.volume;
    public float FltVolumeSFX => audioSFX.volume;

    private void Awake()
    {
        Instance = this;

        fltInitBgmVolume = audioBGM.volume;
        audioBGM.mute = PlayerPrefs.GetInt("UnMuteBGM") == 1;
        audioSFX.mute = PlayerPrefs.GetInt("UnMuteSFX") == 1;
    }

    public void UnMuteBGM()
    {
        audioBGM.mute = !audioBGM.mute;

        PlayerPrefs.SetInt("UnMuteBGM", audioBGM.mute ? 1 : 0);
    }

    public void UnMuteSFX()
    {
        audioSFX.mute = !audioSFX.mute;

        PlayerPrefs.SetInt("UnMuteSFX", audioBGM.mute ? 1 : 0);
    }

    public void SetVolumeBGM(float _fltVolume)
    {
        audioBGM.volume = _fltVolume;
    }

    public void SetVolumeSFX(float _fltVolume)
    {
        audioSFX.volume = _fltVolume;
    }

    public void SetSFX(AudioClip _audioClip)
    {
        StopAllCoroutines();
        audioBGM.volume = fltInitBgmVolume;

        audioSFX.Stop();
        audioSFX.clip = _audioClip;
        audioSFX.Play();
    }

    public void SetSFX(AudioClip _audioClip, float _fltBgmVolumeOnPlay, float _fltDuration)
    {
        SetSFX(_audioClip);

        StartCoroutine(CoroutineChangeBgmVolume(_fltBgmVolumeOnPlay,_fltDuration));
    }

    private IEnumerator CoroutineChangeBgmVolume(float _fltBgmVolumeOnPlay, float _fltDuration)
    {
        audioBGM.volume = _fltBgmVolumeOnPlay;

        yield return new WaitForSeconds(_fltDuration);

        audioBGM.volume = fltInitBgmVolume;
    }
}
