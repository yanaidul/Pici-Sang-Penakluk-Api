using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] GameObject goItem;

    [Space]
    [SerializeField] string strPlayerTag = "Player";

    private void Trigger(Character _character)
    {
        _character.SetItem(goItem);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(strPlayerTag))
            Trigger(other.GetComponent<Character>());
    }
}
