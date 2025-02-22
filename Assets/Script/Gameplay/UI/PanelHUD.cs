using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHUD : MonoBehaviour
{
    private Character character;

    [Header("Reference")]
    private PanelPause panelPause;

    private void Awake()
    {
        panelPause = GetComponent<PanelPause>();
        character = GameManager.Instance.Character;
    }

    public void BtnPause()
    {
        panelPause.Open();
    }

    public void BtnUp()
    {
        character.MoveV(1);
    }

    public void BtnDown()
    {
        character.MoveV(-1);
    }

    public void BtnLeft()
    {
        character.MoveH(-1);
    }

    public void BtnRight()
    {
        character.MoveH(1);
    }

    public void BtnReleaseH()
    {
        character.MoveH(0);
    }

    public void BtnReleaseV()
    {
        character.MoveV(0);
    }
}
