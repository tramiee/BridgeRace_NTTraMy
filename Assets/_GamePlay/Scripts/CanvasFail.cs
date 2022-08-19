using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasFail : UICanvas
{
    public void HomeButton()
    {
        UIManager.Ins.OpenUI(UIID.UICMainMenu);
        SceneManager.LoadScene("MainMenu");
        Close();
    }

    public void RestartButton()
    {

    }
}
