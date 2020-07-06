using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    #region Variables

    private static HUD instance;

    public static HUD Instance { get => instance; set => instance = value; }


    [SerializeField] private GameObject wonWindow;

    public int changeDir;
    #endregion

    private void Start()
    {
        instance = this;
    }

    public void ShowWonWindow(bool parm)
    {
        wonWindow.SetActive(parm);
    }

    public void ButtonPressCheck(int parm)
    {
        changeDir = parm;
    }

    public void PlayAgain()
    {
        ShowWonWindow(false);

        GameConroller.Insnatce.RestartLevel();
    }
}
