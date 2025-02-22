using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFireball : MonoBehaviour
{
    [SerializeField] PoolerContainer pooler;

    [Space]
    [SerializeField] float fltRangeX = 1.5f;
    [SerializeField] float fltRangeZ = 8f;
    private float fltCountdown;

    [Space]
    [SerializeField] int intSpawnPerSec = 1;

    private void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        if(fltCountdown > 0)
        {
            fltCountdown -= Time.deltaTime;
            return;
        }

        Transform transFireball = pooler.Pop().transform;
        transFireball.localPosition = new Vector3(Random.Range(-fltRangeX, fltRangeX), transFireball.localPosition.y, Random.Range(-fltRangeZ, fltRangeZ));
        fltCountdown = 1f;
    }
}
