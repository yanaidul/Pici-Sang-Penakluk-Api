using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelGameover : PanelSimple
{
    public void BtnLevelMenu()
    {
        PlayerPrefs.SetInt("Trigger Panel Level", 1);
        SceneManager.LoadScene("Main Menu");
    }

    public void BtnReplay()
    {
        SceneManager.LoadScene(gameObject.scene.name);
    }
}
