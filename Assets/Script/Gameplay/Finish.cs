using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] string strPlayerTag = "Player";

    private void Trigger()
    {
        GameManager.Instance.Finish();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(strPlayerTag))
            Trigger();
    }
}