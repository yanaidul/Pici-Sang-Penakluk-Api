using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOnTouch : DamageOnTouch
{
    [Space]
    [SerializeField] AudioSource sfxAPAR;

    private bool boolTriggered;

    protected override void Trigger(Collider _other)
    {
        if (boolTriggered) return;

        if (_other.GetComponent<Character>().BoolItem)
            StartCoroutine(CoroutineDead());
        else
            base.Trigger(_other);
    }

    private IEnumerator CoroutineDead()
    {
        boolTriggered = true;

        if(!AudioManager.Instance.BoolSFXMute)
            sfxAPAR.Play();

        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
    }
}
