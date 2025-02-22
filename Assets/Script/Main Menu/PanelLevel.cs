using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelLevel : PanelSimple
{
    [Space]
    [SerializeField] Button[] arrBtnLevel;

    [Space]
    [SerializeField] GameObject[] arrGoImgLock;

    [Space]
    [SerializeField] AudioClip[] arrSfxCaraBermain;

    [Space]
    [SerializeField] string[] arrStrScene;

    [Header("Reference")]
    [SerializeField] SubtitleManager subtitleManager;
    private PanelCaraBermain panelCaraBermain;
    private LevelManager levelManager;

    private void Awake()
    {
        panelCaraBermain = GetComponent<PanelCaraBermain>();
        levelManager = LevelManager.Instance;
    }

    public override void Open()
    {
        if(!levelManager)
            Awake();

        base.Open();

        Refresh();
    }

    public void BtnLevel(int _intId)
    {
        switch(_intId)
        {
            case 0:
                subtitleManager.PlayLevel1Subtitle();
                break;
            case 1:
                subtitleManager.PlayLevel2Subtitle();
                break;
            case 2:
                subtitleManager.PlayLevel3Subtitle();
                break;
        }
        panelCaraBermain.Setup(arrStrScene[_intId], arrSfxCaraBermain[_intId]);
        levelManager.Playing(_intId);

        Close();
    }

    private void Refresh()
    {
        for (int i = 0; i < arrBtnLevel.Length; i++)
        {
            arrBtnLevel[i].interactable = levelManager.IsUnlocked(i);
            arrGoImgLock[i].SetActive(!levelManager.IsUnlocked(i));
        }
    }
}
