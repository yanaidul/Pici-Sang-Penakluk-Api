using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] bool[] arrBoolLevelUnlocked;

    private int intLevelPlaying;

    public void Playing(int _intLevel)
    {
        intLevelPlaying = _intLevel;
    }

    public bool IsUnlocked(int _intLevel)
    {
        return arrBoolLevelUnlocked[_intLevel];
    }

    public void Unlock(int _intLevel = -1)
    {
        if (_intLevel < 0)
            _intLevel = intLevelPlaying + 1;

        if(_intLevel < arrBoolLevelUnlocked.Length)
        {
            arrBoolLevelUnlocked[_intLevel] = true;
            Playing(_intLevel);
        }
    }
}
