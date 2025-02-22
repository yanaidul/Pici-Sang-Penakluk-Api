using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    [SerializeField] FireOnTouch[] arrFireActive;

    [Space]
    [SerializeField] Character character;

    [Header("Reference")]
    [SerializeField] PanelGameover panelGameover;
    [SerializeField] PanelFinish panelFinish;

    public Character Character => character;

    private void Awake()
    {
        Instance = this;
    }

    public void Gameover()
    {
        panelGameover.Open();

        Debug.Log("Gameover");
    }

    public void Finish()
    {
        if(arrFireActive != null)
            foreach (FireOnTouch _fire in arrFireActive)
                if (_fire.gameObject.activeSelf)
                    return;

        panelFinish.Open();

        Debug.Log("Finish");
    }
}
