using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolerFireball : PoolerObject
{
    [SerializeField] Transform transFireball;

    [Space]
    [SerializeField] float fltYPos = 5f;

    private void Update()
    {
        if(transFireball.localPosition.y <= 0)
            gameObject.SetActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        transFireball.localPosition = new Vector3(0, fltYPos, 0);
    }
}
