using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelQuit : PanelSimple
{
    public void BtnYa()
    {
        Application.Quit();
    }

    public void BtnTidak()
    {
        Close();
    }
}
