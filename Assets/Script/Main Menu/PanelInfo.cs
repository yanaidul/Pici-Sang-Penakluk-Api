using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelInfo : PanelSimple
{
    [Space]
    [SerializeField] AudioClip sfxInfo;

    [Header("Reference")]
    private AudioTrigger audioTrigger;

    private void Awake()
    {
        audioTrigger = GetComponent<AudioTrigger>();
    }

    public override void Open()
    {
        base.Open();

        audioTrigger.PlayAudio(sfxInfo);
    }
}
