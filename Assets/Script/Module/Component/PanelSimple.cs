using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSimple : MonoBehaviour
{
    [SerializeField] protected GameObject goPanel;

    [Space]
    [SerializeField] protected PanelSimple panelPrev;

    public virtual void Open()
    {
        goPanel.SetActive(true);
    }

    public virtual void Close()
    {
        goPanel.SetActive(false);
    }

    public void BtnClose()
    {
        Close();

        if(panelPrev)
            panelPrev.Open();
    }
}
