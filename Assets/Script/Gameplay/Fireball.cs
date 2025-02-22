using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float fltSpeed = .1f;

    private void Update()
    {
        transform.position = new Vector3 (transform.position.x, transform.position.y - (Time.deltaTime * fltSpeed), transform.position.z);
    }
}
