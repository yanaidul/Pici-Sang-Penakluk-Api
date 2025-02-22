using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    [SerializeField] string strPlayerTag = "Player";

    protected virtual void Trigger(Collider _other)
    {
        GameManager.Instance.Gameover();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(strPlayerTag))
            Trigger(other);
    }
}
